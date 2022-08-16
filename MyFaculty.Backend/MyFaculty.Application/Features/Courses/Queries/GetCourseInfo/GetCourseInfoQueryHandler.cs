﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.Courses.Queries.GetCourseInfo
{
    public class GetCourseInfoQueryHandler : IRequestHandler<GetCourseInfoQuery, CourseViewModel>
    {
        private IMFDbContext _context;
        private IMapper _mapper;

        public GetCourseInfoQueryHandler(IMFDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CourseViewModel> Handle(GetCourseInfoQuery request, CancellationToken cancellationToken)
        {
            Course course = await _context.Courses.FirstOrDefaultAsync(course => course.Id == request.Id, cancellationToken);
            if (course == null)
                throw new EntityNotFoundException(nameof(Course), request.Id);
            return _mapper.Map<CourseViewModel>(course);
        }
    }
}
