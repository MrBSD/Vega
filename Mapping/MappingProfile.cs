using System.Linq;
using AutoMapper;
using Vega.Controllers.Resources;
using Vega.Models;

namespace Vega.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            //Domain to API Resources
            CreateMap<Make, MakeResource>();
            CreateMap<Model, ModelResource>();
            CreateMap<Feature, FeatureResource>();
            CreateMap<Vehicle, VehicleResource>()
            .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource{Name=v.ContactName, Phone = v.ContactPhone, Email=v.ContactEmail}))
            .ForMember(vr=> vr.Features, opt => opt.MapFrom(v=> v.Features.Select(vf=>vf.FeatureId)));


            //API Resource to Domain
            CreateMap<VehicleResource, Vehicle>()
            .ForMember(vr=>vr.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
            .ForMember(vr=>vr.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
            .ForMember(vr=>vr.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
            .ForMember(vr=>vr.Features, opt => opt.MapFrom(vr => vr.Features.Select(id=>new VehicleFeature{FeatureId=id})));
        }
    }
}