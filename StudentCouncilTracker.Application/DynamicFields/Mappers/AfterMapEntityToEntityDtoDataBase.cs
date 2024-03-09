using AutoMapper;
using StudentCouncilTracker.Application.DynamicFields.Helpers;
using StudentCouncilTracker.Application.Entities.Events.Domain;
using StudentCouncilTracker.Application.Entities.UserRoles.Enums;
using StudentCouncilTracker.Application.Extensions;
using StudentCouncilTracker.Application.Services.UserProviders;

namespace StudentCouncilTracker.Application.DynamicFields.Mappers;

public abstract class AfterMapEntityToEntityDtoDataBase<TSource, TDestination>(IUserProvider userProvider) : IMappingAction<TSource, TDestination>
{
    public void Process(TSource source, TDestination destination, ResolutionContext context)
    {
        if (source is Event eventData)
        {
            var isChairman = userProvider.Role == Role.Chairman;
            var isResponsibleUser = userProvider.Name == eventData.ResponsibleUser?.Name;

            var dynProps = new List<DynamicFieldInfo>();

            var fields = DynamicFieldHelper.CreateFields(eventData, destination!, dynProps, new List<string>());
            var destProps = TypeHolder.GetProperties(destination?.GetType());
            foreach (var prop in destProps)
            {
                if (prop.PropertyType.BaseType != typeof(DynamicField))
                    continue;

                var pName = prop.Name;
                var field = fields.Find(f => f.FieldName.Equals(pName, StringComparison.OrdinalIgnoreCase)) ??
                            DynamicFieldHelper.CreateDefaultField(pName);

                if (prop.GetValue(destination) is not DynamicField v)
                {
                    var v1 = Activator.CreateInstance(prop.PropertyType);
                    prop.SetValue(destination, v1);
                    v = v1 as DynamicField;
                }

                if (v == null)
                    continue;

                v.IsVisible = field.IsVisible;
                v.IsEditable = (field.IsEditable && (isChairman || isResponsibleUser)) ;
                v.IsValueHidden = field.IsValueHidden;
                v.Type = field.Type;
                v.Label.Label = field.Label!;
                v.Label.LabelHint = field.LabelHint!;
                v.Name = pName.FirstCharToLower();
            }
        }
        else
        {
            var dynProps = new List<DynamicFieldInfo>();

            var fields = DynamicFieldHelper.CreateFields(source, destination, dynProps, new List<string>());
            var destProps = TypeHolder.GetProperties(destination.GetType());
            foreach (var prop in destProps)
            {
                if (prop.PropertyType.BaseType != typeof(DynamicField))
                    continue;

                var pName = prop.Name;
                var field = fields.Find(f => f.FieldName.Equals(pName, StringComparison.OrdinalIgnoreCase)) ??
                            DynamicFieldHelper.CreateDefaultField(pName);

                if (prop.GetValue(destination) is not DynamicField v)
                {
                    var v1 = Activator.CreateInstance(prop.PropertyType);
                    prop.SetValue(destination, v1);
                    v = v1 as DynamicField;
                }

                if (v == null)
                    continue;

                v.IsVisible = field.IsVisible;
                v.IsEditable = field.IsEditable;
                v.IsValueHidden = field.IsValueHidden;
                v.Type = field.Type;
                v.Label.Label = field.Label!;
                v.Label.LabelHint = field.LabelHint!;
                v.Name = pName.FirstCharToLower();
            }
        }
    }
}