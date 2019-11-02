// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.


function initOpenLayer() {

  map = new ol.Map("mapdiv");
  map.addLayer(new ol.Layer.OSM());

  var lonLat = new olenLayers.LonLat( -0.1279688 ,51.5077286 )
    .transform(
      new ol.Projection("EPSG:4326"), // transform from WGS 1984
      map.getProjectionObject() // to Spherical Mercator Projection
    );

  var zoom=16;

  var markers = new ol.Layer.Markers( "Markers" );
  map.addLayer(markers);

  markers.addMarker(new ol.Marker(lonLat));

  map.setCenter (lonLat, zoom);
}
