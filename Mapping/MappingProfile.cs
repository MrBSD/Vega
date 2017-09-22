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
            CreateMap<Make, KeyValuePairResource>();
            CreateMap<Model, KeyValuePairResource>();
            CreateMap<Feature, KeyValuePairResource>();
            CreateMap<Vehicle, SaveVehicleResource>()
                .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource{Name=v.ContactName, Phone = v.ContactPhone, Email=v.ContactEmail}))
                .ForMember(vr=> vr.Features, opt => opt.MapFrom(v => v.Features.Select(vr => vr.FeatureId)));
            CreateMap<Vehicle,VehicleResource>()
                .ForMember(vr => vr.Make, opt => opt.MapFrom(v => v.Model.Make))
                .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource{Name=v.ContactName, Phone = v.ContactPhone, Email=v.ContactEmail}))
                .ForMember(vr=> vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => new KeyValuePairResource{Id = vf.FeatureId, Name=vf.Feature.Name})));

            //API Resource to Domain
            CreateMap<SaveVehicleResource, Vehicle>()
            .ForMember(vr => vr.Id, opt => opt.Ignore())
            .ForMember(vr=>vr.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
            .ForMember(vr=>vr.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
            .ForMember(vr=>vr.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
            .ForMember(vr=>vr.Features, opt => opt.Ignore())
             .AfterMap((vr, v) =>
             {
                 //removing feature
                 var removedFeatures = v.Features
                 .Where(f => !vr.Features
                 .Contains(f.FeatureId)).ToList();

                 foreach (var f in removedFeatures)
                 {
                     v.Features.Remove(f);
                 }

                 //Add new fetures
                 var addedFeatures = vr.Features
                 .Where(id => !v.Features
                 .Any(f => f.FeatureId == id))
                 .Select(id => new VehicleFeature{FeatureId = id} )
                .ToList();

                 foreach (var f in addedFeatures)
                 {
                     v.Features.Add(f);
                 }
             });
        }
    }
}