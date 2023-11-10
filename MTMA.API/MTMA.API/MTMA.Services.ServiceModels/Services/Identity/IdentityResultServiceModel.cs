namespace MTMA.Services.ServiceModels
{
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using MTMA.Services.Mapping;

    public class IdentityResultServiceModel : IMapFrom<IdentityResult>, IHaveCustomMappings
    {
        public bool Succeeded { get; set; }

        public List<KeyValuePair<string, string>> Errors { get; set; } = new List<KeyValuePair<string, string>>();

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<IdentityResult, IdentityResultServiceModel>().ForMember(
                destination => destination.Errors,
                opt => opt.MapFrom(x => x.Errors.Select(t => new KeyValuePair<string, string>(t.Code, t.Description))));
        }
    }
}
