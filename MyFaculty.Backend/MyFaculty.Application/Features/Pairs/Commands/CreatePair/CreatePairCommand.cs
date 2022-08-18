using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MyFaculty.Application.ViewModels;
using MyFaculty.Domain.Entities;

namespace MyFaculty.Application.Features.Pairs.Commands.CreatePair
{
    public class CreatePairCommand : IRequest<PairViewModel>
    {
        public string PairName { get; set; }
        public int TeacherId { get; set; }
        public int AuditriumId { get; set; }
        public int GroupId { get; set; }
        public int DisciplineId { get; set; }
        public int DayOfWeekId { get; set; }
        public int PairRepeatingId { get; set; }
        public int PairInfoId { get; set; }
    }
}
