using MediatR;
using MyFaculty.Application.Common.Exceptions;
using MyFaculty.Application.Common.Interfaces;
using MyFaculty.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace MyFaculty.Application.Features.Teachers.Commands.DeleteTeacher
{
    public class DeleteTeacherCommandHandler : IRequestHandler<DeleteTeacherCommand>
    {
        private IMFDbContext _context;

        public DeleteTeacherCommandHandler(IMFDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
        {
            Teacher teacher = await _context.Teachers.FindAsync(new object[] { request.Id });
            if (teacher == null)
                throw new EntityNotFoundException(nameof(Teacher), request.Id);
            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
