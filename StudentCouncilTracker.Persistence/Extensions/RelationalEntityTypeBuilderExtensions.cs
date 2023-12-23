using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StudentCouncilTracker.Persistence.Extensions;

public static class RelationalEntityTypeBuilderExtensions
{
    public static EntityTypeBuilder ToSchema(this EntityTypeBuilder entityTypeBuilder, string? name)
    {
        if (name is not null && name.Length == 0)
        {
            throw new ArgumentException(AbstractionsStrings.ArgumentIsEmpty(name));
        }

        entityTypeBuilder.Metadata.SetSchema(name);

        return entityTypeBuilder;
    }
}