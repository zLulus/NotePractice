using System;
using System.Collections.Generic;
using System.Diagnostics;
using GeoAPI.Geometries;
using GeoJSON.Net;
using NetTopologySuite.Geometries;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CoreWebsite.Converters
{
    public class GeometryConverter : JsonConverter
    {
        private readonly IGeometryFactory _factory;

        public GeometryConverter() : this(GeometryFactory.Default) { }

        public GeometryConverter(IGeometryFactory geometryFactory)
        {
            _factory = geometryFactory;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            IGeometry geom = value as IGeometry;
            if (geom == null)
                return;

            writer.WriteStartObject();

            GeoJSONObjectType geomType = ToGeoJsonObject(geom);
            writer.WritePropertyName("type");
            writer.WriteValue(Enum.GetName(typeof(GeoJSONObjectType), geomType));

            switch (geomType)
            {
                case GeoJSONObjectType.Point:
                    serializer.Serialize(writer, geom.Coordinate);
                    break;
                case GeoJSONObjectType.LineString:
                case GeoJSONObjectType.MultiPoint:
                    serializer.Serialize(writer, geom.Coordinates);
                    break;
                case GeoJSONObjectType.Polygon:
                    IPolygon poly = geom as IPolygon;
                    Debug.Assert(poly != null);
                    serializer.Serialize(writer, PolygonCoordiantes(poly));
                    break;

                case GeoJSONObjectType.MultiPolygon:
                    IMultiPolygon mpoly = geom as IMultiPolygon;
                    Debug.Assert(mpoly != null);
                    List<List<Coordinate[]>> list = new List<List<Coordinate[]>>();
                    foreach (IPolygon mempoly in mpoly.Geometries)
                        list.Add(PolygonCoordiantes(mempoly));
                    serializer.Serialize(writer, list);
                    break;

                case GeoJSONObjectType.GeometryCollection:
                    IGeometryCollection gc = geom as IGeometryCollection;
                    Debug.Assert(gc != null);
                    serializer.Serialize(writer, gc.Geometries);
                    break;
                default:
                    List<Coordinate[]> coordinates = new List<Coordinate[]>();
                    foreach (IGeometry geometry in ((IGeometryCollection)geom).Geometries)
                        coordinates.Add(geometry.Coordinates);
                    serializer.Serialize(writer, coordinates);
                    break;
            }

            writer.WriteEndObject();
        }



        private GeoJSONObjectType ToGeoJsonObject(IGeometry geom)
        {
            if (geom is IPoint)
                return GeoJSONObjectType.Point;
            if (geom is ILineString)
                return GeoJSONObjectType.LineString;
            if (geom is IPolygon)
                return GeoJSONObjectType.Polygon;
            if (geom is IMultiPoint)
                return GeoJSONObjectType.MultiPoint;
            if (geom is IMultiLineString)
                return GeoJSONObjectType.MultiLineString;
            if (geom is IMultiPolygon)
                return GeoJSONObjectType.MultiPolygon;
            if (geom is IGeometryCollection)
                return GeoJSONObjectType.GeometryCollection;
            throw new ArgumentException("geom");
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            reader.Read();
            if (!(reader.TokenType == JsonToken.PropertyName && (string)reader.Value == "type"))
                throw new ArgumentException("invalid tokentype: " + reader.TokenType);
            reader.Read();
            if (reader.TokenType != JsonToken.String)
                throw new ArgumentException("invalid tokentype: " + reader.TokenType);
            GeoJSONObjectType geometryType = (GeoJSONObjectType)Enum.Parse(typeof(GeoJSONObjectType), (string)reader.Value);
            switch (geometryType)
            {
                case GeoJSONObjectType.Point:
                    Coordinate coordinate = serializer.Deserialize<Coordinate>(reader);
                    return _factory.CreatePoint(coordinate);

                case GeoJSONObjectType.LineString:
                    Coordinate[] coordinates = serializer.Deserialize<Coordinate[]>(reader);
                    return _factory.CreateLineString(coordinates);

                case GeoJSONObjectType.Polygon:
                    List<Coordinate[]> coordinatess = serializer.Deserialize<List<Coordinate[]>>(reader);
                    return CreatePolygon(coordinatess);

                case GeoJSONObjectType.MultiPoint:
                    coordinates = serializer.Deserialize<Coordinate[]>(reader);
                    return _factory.CreateMultiPoint(coordinates);

                case GeoJSONObjectType.MultiLineString:
                    coordinatess = serializer.Deserialize<List<Coordinate[]>>(reader);
                    List<ILineString> strings = new List<ILineString>();
                    for (int i = 0; i < coordinatess.Count; i++)
                        strings.Add(_factory.CreateLineString(coordinatess[i]));
                    return _factory.CreateMultiLineString(strings.ToArray());

                case GeoJSONObjectType.MultiPolygon:
                    List<List<Coordinate[]>> coordinatesss = serializer.Deserialize<List<List<Coordinate[]>>>(reader);
                    List<IPolygon> polygons = new List<IPolygon>();
                    foreach (List<Coordinate[]> coordinateses in coordinatesss)
                        polygons.Add(CreatePolygon(coordinateses));
                    return _factory.CreateMultiPolygon(polygons.ToArray());

                case GeoJSONObjectType.GeometryCollection:
                    List<IGeometry> geoms = serializer.Deserialize<List<IGeometry>>(reader);
                    return _factory.CreateGeometryCollection(geoms.ToArray());
            }
            return null;
        }

        private static List<Coordinate[]> PolygonCoordiantes(IPolygon polygon)
        {
            List<Coordinate[]> res = new List<Coordinate[]>();
            res.Add(polygon.Shell.Coordinates);
            foreach (ILineString interiorRing in polygon.InteriorRings)
                res.Add(interiorRing.Coordinates);
            return res;
        }

        private IPolygon CreatePolygon(IList<Coordinate[]> coordinatess)
        {
            ILinearRing shell = _factory.CreateLinearRing(coordinatess[0]);
            List<ILinearRing> rings = new List<ILinearRing>();
            for (int i = 1; i < coordinatess.Count; i++)
                rings.Add(_factory.CreateLinearRing(coordinatess[i]));
            return _factory.CreatePolygon(shell, rings.ToArray());
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IGeometry).IsAssignableFrom(objectType);
        }
    }
}
