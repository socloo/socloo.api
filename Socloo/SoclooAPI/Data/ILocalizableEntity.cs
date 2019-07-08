using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoclooAPI.Data
{
    public interface ILocalizableEntity<T> : IEntity<T>
    {
        //GeoJsonPoint<GeoJson2DGeographicCoordinates> Location { get; set; }

        T UserId { get; set; }

        string[] Tags { get; set; }
    }
}
