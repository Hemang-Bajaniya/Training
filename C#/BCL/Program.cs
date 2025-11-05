using System.Net.Http.Json;

namespace BCL
{
    internal class Program
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public static async Task<int> Main(string[] args)
        {
            Console.WriteLine("HttpClient demo - .NET\n");

            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("HttpClient-Demo/1.0");

            while (true)
            {
                Console.WriteLine("Choose an example to run:");
                Console.WriteLine("1) Simple GET (string)");
                Console.WriteLine("2) GET and deserialize JSON (Post)");
                Console.WriteLine("3) POST JSON (create Post)");
                Console.WriteLine("4) GET with timeout and cancellation");
                Console.WriteLine("5) Exit");
                Console.Write("> ");
                var choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            await SimpleGetAsync();
                            break;
                        case "2":
                            await GetAndDeserializeJsonAsync();
                            break;
                        case "3":
                            await PostJsonAsync();
                            break;
                        case "4":
                            await GetWithTimeoutAndCancellationAsync();
                            break;
                        case "5":
                            Console.WriteLine("Goodbye.");
                            return 0;
                        default:
                            Console.WriteLine("Unknown choice, try again.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.GetType().Name}: {ex.Message}");
                }

                Console.WriteLine();
            }
        }

        private static async Task SimpleGetAsync()
        {
            Console.WriteLine("Running simple GET against https://example.com...");
            using var response = await _httpClient.GetAsync("https://example.com");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Status: {(int)response.StatusCode} {response.ReasonPhrase}");
            Console.WriteLine("--- Body (first 500 chars) ---");
            Console.WriteLine(content.Length > 500 ? content.Substring(0, 500) + "..." : content);
        }

        private static async Task GetAndDeserializeJsonAsync()
        {
            Console.WriteLine("GET JSON from jsonplaceholder.typicode.com/posts/1 and deserialize to Post object...");
            var url = "https://jsonplaceholder.typicode.com/posts/1";

            var post = await _httpClient.GetFromJsonAsync<Post>(url);
            if (post is null)
            {
                Console.WriteLine("No data returned.");
                return;
            }

            Console.WriteLine($"Received Post: Id={post.Id}, Title={post.Title}");
            Console.WriteLine("Body (first 200 chars):");
            Console.WriteLine(post.Body?.Length > 200 ? post.Body.Substring(0, 200) + "..." : post.Body);
        }

        private static async Task PostJsonAsync()
        {
            Console.WriteLine("POST JSON to jsonplaceholder.typicode.com/posts (this is a fake API that returns the created object)...");
            var url = "https://jsonplaceholder.typicode.com/posts";

            var newPost = new Post
            {
                UserId = 123,
                Title = "Hello from HttpClient demo",
                Body = "This is a sample post created by the HttpClient demo program."
            };

            using var response = await _httpClient.PostAsJsonAsync(url, newPost);
            response.EnsureSuccessStatusCode();

            var created = await response.Content.ReadFromJsonAsync<Post>();
            Console.WriteLine($"Created resource with server Id={created?.Id}");
            Console.WriteLine("Full response JSON:");
            var raw = await response.Content.ReadAsStringAsync();
            Console.WriteLine(raw);
        }

        private static async Task GetWithTimeoutAndCancellationAsync()
        {
            Console.WriteLine("GET with timeout and user cancellation. Will call https://httpbin.org/delay/5 which waits 5s before responding.");
            var url = "https://httpbin.org/delay/5"; // endpoint that delays the response

            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(2));

            try
            {

                using var response = await _httpClient.GetAsync(url, cts.Token);
                response.EnsureSuccessStatusCode();
                Console.WriteLine("Request completed before timeout.");
            }
            catch (OperationCanceledException)
            {
                if (cts.IsCancellationRequested)
                {
                    Console.WriteLine("Request cancelled by timeout (cts).");
                }
                else
                {
                    Console.WriteLine("Request cancelled (unknown reason).");
                }
            }
        }

        private class Post
        {
            public int UserId { get; set; }
            public int Id { get; set; }
            public string Title { get; set; } = string.Empty;
            public string Body { get; set; } = string.Empty;
        }
    }
}
