//using Xunit;
//using System.Net;
//using System.Net.Http.Json;
//using Microsoft.AspNetCore.Mvc.Testing;
//using FluentAssertions;
//using ReadingRoomManager;
//using ReadingRoomManager.Entities;

//public class ReservationEndpointsTests : IClassFixture<WebApplicationFactory<Program>>
//{
//    private readonly WebApplicationFactory<Program> _factory;
//    private readonly HttpClient _client;

//    public ReservationEndpointsTests(WebApplicationFactory<Program> factory)
//    {
//        _factory = factory.WithWebHostBuilder(builder => { }); // use test server
//        _client = _factory.CreateClient();
//    }

//    [Fact]
//    public async Task PostReservation_ShouldReturnCreated()
//    {
//        // Arrange
//        var room = new Room { Name = "Test Room", Capacity = 5 };
//        var createRoom = await _client.PostAsJsonAsync("/rooms", room);
//        createRoom.StatusCode.Should().Be(HttpStatusCode.Created);
//        var createdRoom = await createRoom.Content.ReadFromJsonAsync<Room>();

//        var reservation = new Reservation
//        {
//            RoomId = createdRoom!.Id,
//            Start = DateTime.UtcNow,
//            End = DateTime.UtcNow.AddHours(2),
//            Status = ReservationStatus.Pending
//        };

//        // Act
//        var response = await _client.PostAsJsonAsync("/reservations", reservation);

//        // Assert
//        response.StatusCode.Should().Be(HttpStatusCode.Created);
//        var created = await response.Content.ReadFromJsonAsync<Reservation>();
//        created!.RoomId.Should().Be(createdRoom.Id);
//    }

//    [Fact]
//    public async Task GetRooms_ShouldReturnOkAndList()
//    {
//        var response = await _client.GetAsync("/rooms");
//        response.StatusCode.Should().Be(HttpStatusCode.OK);
//        var rooms = await response.Content.ReadFromJsonAsync<List<Room>>();
//        rooms.Should().NotBeNull();
//    }
//}
