using System.Collections.Generic;
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
            .ForMember(vr=> vr.Features, opt => opt.MapFrom(v => v.Features.Select(vr => vr.FeatureId)));
           


            //API Resource to Domain
            CreateMap<VehicleResource, Vehicle>()
            .ForMember(vr => vr.Id, opt => opt.Ignore())
            .ForMember(vr=>vr.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
            .ForMember(vr=>vr.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
            .ForMember(vr=>vr.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
            .ForMember(vr=>vr.Features, opt => opt.Ignore())
             .AfterMap((vr ,v ) =>{
                //removing feature
                var removedFeatures = new List<VehicleFeature>();
                foreach (var f in v.Features)
                {
                    if (!vr.Features.Contains(f.FeatureId))
                    {
                        removedFeatures.Add(f);
                    }
                }

                foreach (var f in removedFeatures)
                {
                    v.Features.Remove(f);
                }

                //Add new fetures
                foreach (var id in vr.Features)
                {
                    if (!v.Features.Any(f => f.FeatureId==id));
                        v.Features.Add(new VehicleFeature{FeatureId=id});
                }
            });
        }
    }
}