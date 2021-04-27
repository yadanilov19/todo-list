using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.TodoItems.Commands.CreateTodoItem;
using CleanArchitecture.Application.TodoItems.Commands.UpdateTodoItem;
using CleanArchitecture.Application.TodoItems.Commands.UpdateTodoItemDetail;
using CleanArchitecture.Application.TodoLists.Commands.CreateTodoList;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;
using System;

namespace CleanArchitecture.Application.IntegrationTests.TodoItems.Commands
{
    using static Testing;

    public class UpdateTodoItemDetailTests : TestBase
    {
        [Test]
        public void ShouldRequireValidTodoItemId()
        {
            var command = new UpdateTodoItemCommand
            {
                Id = 99,
                Title = "New Title"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldUpdateTodoItem()
        {
            var userId = await RunAsDefaultUserAsync();

            var listId = await SendAsync(new CreateTodoListCommand
            {
                Title = "New List"
            });

            var itemRootId = await SendAsync(new CreateTodoItemCommand
            {
                ListId = listId,
                Title = "New Item Root"
            });

            const string newItemTitle = "New Item";
            var itemId = await SendAsync(new CreateTodoItemCommand
            {
                ListId = listId,
                Title = newItemTitle
            });

            var expiryDate = new DateTime(2000, 1, 1);
            var command = new UpdateTodoItemDetailCommand
            {
                Id = itemRootId,
                ListId = listId,
                Note = "This is the note.",
                Priority = PriorityLevel.High,
                ExpiryDate = expiryDate,
                TodoRefId = itemId
            };

            await SendAsync(command);


            var itemRoot = await FindTodoFullInfoAsync(itemRootId);
            itemRoot.ListId.Should().Be(command.ListId);
            itemRoot.Note.Should().Be(command.Note);
            itemRoot.Priority.Should().Be(command.Priority);
            itemRoot.LastModifiedBy.Should().NotBeNull();
            itemRoot.LastModifiedBy.Should().Be(userId);
            itemRoot.LastModified.Should().NotBeNull();
            itemRoot.LastModified.Should().BeCloseTo(DateTime.Now, 10000);
            itemRoot.ExpiryDate.Should().Be(expiryDate);
            
            itemRoot.TodoItemRef.Should().NotBeNull();
            itemRoot.TodoItemRef.Title.Should().Be(newItemTitle);
        }
    }
}
