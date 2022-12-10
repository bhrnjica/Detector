using Detector.Contract.DTO;
using Detector.Contract.Models;
using Detector.Test.Extensions;
using Detector.WebApi.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Detector.Test.Integration;

public class DetectorTests
{
    DetectorData createDetector()
    {
        return new DetectorData
        {
            Name = "Detector01",
            Version = 1,
            DetectorDetails = new DetectorDetails
            {
                Notes = "Nokia3310"
            }
        };

    }

    DetectorItem createDetectorItem()
    {
        return new DetectorItem
        {
            Id = 1,
            Name = "Detector01",
            Version = 1,
            Notes = "Nokia3310"
        };

    }

    DetectorCreateRequest createDetectorRequest()
    {
        return new DetectorCreateRequest
        {
            Name = "Detector01",
            Version = 1,
            Notes = "Nokia3310"
        };

    }

    [Fact]
    public async Task Detector_GetAll_With_Proper_Authentication()
    {
        var userId = "34";

        await using var application = new DetectorApp();
        await using var db = application.CreateDetectorContext();

        await application.CreateUserAsync(userId);

        db.DetectorData.Add(createDetector());
        await db.SaveChangesAsync();

        var client = application.CreateClient(userId);
        var detectors = await client.GetFromJsonAsync<List<DetectorItem>>("/detectors");
        Assert.NotNull(detectors);

        var detector = Assert.Single(detectors);
        Assert.Equal("Detector01", detector.Name);
    }

    [Fact]
    public async Task Detector_GetAll_WithoutAuthorization()
    {
        var userId = "34";

        await using var application = new DetectorApp();
        await using var db = application.CreateDetectorContext();

        var client = application.CreateClient(userId);
        var response = await client.GetAsync("/detectors");

        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }
    [Fact]
    public async Task Detector_Put()
    {
        var userId = "34";
        await using var application = new DetectorApp();
        await using var db = application.CreateDetectorContext();
        await application.CreateUserAsync(userId);

        db.DetectorData.Add(createDetector());
        var entity = await db.SaveChangesAsync();

        var client = application.CreateClient(userId);
        var detectors = await client.GetFromJsonAsync<List<DetectorItem>>("/detectors");
        Assert.NotNull(detectors);

        var detector = Assert.Single(detectors);
        Assert.Equal("Detector01", detector.Name);

        var item = new DetectorUpdateRequest();
        item.Id= detector.Id;
        item.Name = "Detector02";
        item.Version = 3;
        item.Notes = "Nokia2022";

        var response = await client.PutAsJsonAsync($"/detectors/{item.Id}", item);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        detectors = await client.GetFromJsonAsync<List<DetectorItem>>("/detectors");
        Assert.NotNull(detectors);

        detector = Assert.Single(detectors);
        Assert.Equal("Detector02", detector.Name);


    }
    [Fact]
    public async Task Detector_Post()
    {
        var userId = "34";
        await using var application = new DetectorApp();
        await using var db = application.CreateDetectorContext();
        await application.CreateUserAsync(userId);

        var client = application.CreateClient(userId);
        var detectoritem = createDetectorRequest();


        var response = await client.PostAsJsonAsync($"/detectors", detectoritem);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var detectors = await client.GetFromJsonAsync<List<DetectorItem>>("/detectors");
        Assert.NotNull(detectors);

        var detector = Assert.Single(detectors);
        Assert.Equal("Detector01", detector.Name);
    }

    [Fact]
    public async Task Detector_Delete()
    {
        var userId = "34";

        await using var application = new DetectorApp();
        await using var db = application.CreateDetectorContext();

        await application.CreateUserAsync(userId);

        db.DetectorData.Add(createDetector());

        await db.SaveChangesAsync();

        var client = application.CreateClient(userId);

        var detector = db.DetectorData.FirstOrDefault();
        Assert.NotNull(detector);
        Assert.Equal("Detector01", detector.Name);


        var response = await client.DeleteAsync($"/detectors/{detector.Id}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        detector = db.DetectorData.FirstOrDefault();
        Assert.Null(detector);
    }
}
