﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper.Configuration;

namespace AutoMapper.QueryableExtensions
{
    using static Expression;

    internal class NullableSourceExpressionBinder : IExpressionBinder
    {
        public MemberAssignment Build(IConfigurationProvider configuration, PropertyMap propertyMap, TypeMap propertyTypeMap, ExpressionRequest request, ExpressionResolutionResult result, IDictionary<ExpressionRequest, int> typePairCount)
        {
            var defaultDestination = Activator.CreateInstance(propertyMap.DestinationPropertyType);
            return Bind(propertyMap.DestinationProperty, Coalesce(result.ResolutionExpression, Constant(defaultDestination)));
        }

        public bool IsMatch(PropertyMap propertyMap, TypeMap propertyTypeMap, ExpressionResolutionResult result) =>
            result.Type.IsNullableType() && !propertyMap.DestinationPropertyType.IsNullableType();
    }
}