const http = require('http');
const fs = require('fs');

function main() {
    const file = fs.createWriteStream("map.osm");
    /*right format is 
        bbox=left, bottom, right, top

        eg: this example pull map data in Ho Chi Minh city
        https://api.openstreetmap.org/api/0.6/map?bbox=106.5543,10.6741,106.9032,10.8606
    */
    //https://api.openstreetmap.org/api/0.6/map?bbox=10.6741,106.9032,10.8606,106.5543
    const req = http.get("http://overpass.openstreetmap.ru/cgi/xapi_meta?*[bbox=106.5543,10.6741,106.9032,10.8606]", (res) =>{
        res.pipe(file);
        console.log('Pulled');
    });
}

main();