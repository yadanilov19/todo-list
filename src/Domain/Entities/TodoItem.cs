using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Events;
using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities
{
    public class TodoItem : AuditableEntity, IHasDomainEvent
    {
        public int Id { get; set; }

        public virtual TodoList List { get; set; }

        public int ListId { get; set; }
        public string Title { get; set; }

        public string Note { get; set; }
        
        public PriorityLevel Priority { get; set; }

        public DateTime? ExpiryDate { get; set; }
        public int? TodoItemRefId { get; set; }
        public virtual TodoItem TodoItemRef { get; set; }


        private bool _done;
        public bool Done
        {
            get => _done;
            set
            {
                if (value == true && _done == false)
                {
                    DomainEvents.Add(new TodoItemCompletedEvent(this));
                }

                _done = value;
            }
        }
        
        public List<DomainEvent> DomainEvents { get; set; } = new List<DomainEvent>();
    }
}
