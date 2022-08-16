﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.Courses.Commands.DeleteCourse
{
    public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand>
    {
        private IMFDbContext _context;

        public DeleteCourseCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            Course course = await _context.Courses.FindAsync(new object[] { request.Id }, cancellationToken);
            if (course == null)
                throw new EntityNotFoundException(nameof(Course), request.Id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
