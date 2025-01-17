﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using SoftSquare.AlAhlyClub.Application.Common.Interfaces.Serialization;
using SoftSquare.AlAhlyClub.Application.Features.Documents.Caching;
using SoftSquare.AlAhlyClub.Domain.Common.Enums;

namespace SoftSquare.AlAhlyClub.Infrastructure.Services.PaddleOCR;

public class DocumentOcrJob : IDocumentOcrJob
{
    private readonly IApplicationDbContext _context;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<DocumentOcrJob> _logger;
    private readonly IApplicationHubWrapper _notificationService;
    private readonly ISerializer _serializer;
    private readonly Stopwatch _timer;

    public DocumentOcrJob(
        IApplicationHubWrapper appNotificationService,
        IApplicationDbContext context,
        IHttpClientFactory httpClientFactory,
        ISerializer serializer,
        ILogger<DocumentOcrJob> logger)
    {
        _notificationService = appNotificationService;
        _context = context;
        _httpClientFactory = httpClientFactory;
        _serializer = serializer;
        _logger = logger;
        _timer = new Stopwatch();
    }

    public void Do(int id)
    {
        Recognition(id, CancellationToken.None).Wait();
    }

    public async Task Recognition(int id, CancellationToken cancellationToken)
    {
        try
        {
            using (var client = _httpClientFactory.CreateClient("ocr"))
            {
                _timer.Start();
                var doc = await _context.Documents.FindAsync(id);
                if (doc == null) return;
                await _notificationService.JobStarted(doc.Title!);
                DocumentCacheKey.SharedExpiryTokenSource().Cancel();
                if (string.IsNullOrEmpty(doc.URL)) return;
                var imgFile = Path.Combine(Directory.GetCurrentDirectory(), doc.URL);
                if (!File.Exists(imgFile)) return; 
                using var form = new MultipartFormDataContent();
                using var fileStream = new FileStream(imgFile, FileMode.Open);
                using var fileContent = new StreamContent(fileStream);
                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/png");
                form.Add(fileContent, "file", Path.GetFileName(imgFile)); // "image" is the form parameter name for the file

                var response = await client.PostAsync("", form);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    doc.Status = JobStatus.Done;
                    doc.Description = "recognize the result: success";
                    doc.Content = result;


                    await _context.SaveChangesAsync(cancellationToken);
                    await _notificationService.JobCompleted(doc.Title!);
                    DocumentCacheKey.SharedExpiryTokenSource().Cancel();
                    _timer.Stop();
                    var elapsedMilliseconds = _timer.ElapsedMilliseconds;
                    _logger.LogInformation("Image recognition completed. Id: {id}, Elapsed Time: {elapsedMilliseconds}ms, Status: {StatusCode}",
                        id, elapsedMilliseconds, response.StatusCode);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{id}: Image recognize error {Message}", id, ex.Message);
        }
    }
}
#pragma warning disable CS8981
internal class OcrResult
{
    [JsonPropertyName("resultcode")]
    public string? ResultCode { get; set; }
    [JsonPropertyName("message")]
    public string? Message { get; set; }
    [JsonPropertyName("data")]
    public List<List<List<dynamic>>>? Data { get; set; }
}
#pragma warning restore CS8981