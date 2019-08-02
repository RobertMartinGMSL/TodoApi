using System.Collections.Generic;
using System.Linq;

namespace ToDoIntegrationTests
{
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using TodoApi;
    using TodoApi.Models;

    public class Tests
    {
        private WebApplicationFactory<Program> _webApplicationFactory;

        [SetUp]
        public void Setup()
        {
            this._webApplicationFactory = new WebApplicationFactory<Program>();
        }

        [Test]
        public async Task Test1()
        {
            using (var client = this._webApplicationFactory.CreateClient())
            {
                var response = await client.GetAsync("/api/todo");

                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

                var jsonString = await response.Content.ReadAsStringAsync();
                var todoItems = JsonConvert.DeserializeObject<List<TodoItem>>(jsonString);

                Assert.That(todoItems.First().Id, Is.EqualTo(1));
                Assert.That(todoItems.First().IsComplete, Is.False);
                Assert.That(todoItems.First().Name, Is.EqualTo("Item1"));

                Assert.That(todoItems.Last().Id, Is.EqualTo(2));
                Assert.That(todoItems.Last().IsComplete, Is.False);
                Assert.That(todoItems.Last().Name, Is.EqualTo("Item2"));
            }
        }
    }
}