﻿using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Activity Activity{ get; set; }
        }
        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handler(DataContext context )
            {
                _context = context;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.Activity.Id);
                activity.Title = request.Activity.Title ?? activity.Title;
                activity.Date = request.Activity.Date;
                activity.Description = request.Activity.Description ?? activity.Description;
                activity.Category = request.Activity.Category ?? activity.Category;
                activity.City = request.Activity.City ?? activity.City;
                activity.Venue = request.Activity.Venue ?? activity.Venue;
                //_mapper.Map(request.Activity , activity);

                await _context.SaveChangesAsync();
            }
        }
    }
}
