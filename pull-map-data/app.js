const http = require('http');
const fs = require('fs');
const convert = require('xml-js');
const path = require('path');
const buildUrl = require('build-url');
const log4js = require('log4js');
const logger = log4js.getLogger();
logger.level = 'debug';

function mkdirs(folders) {
    return new Promise((resolve, reject) => {
        let today = new Date().getDate() + "-" + new Date().getTime();
        let root = 'pulled';
        if (!fs.existsSync(root)) {
            fs.mkdirSync(root);
        }
    
        let currentDir = path.join(root, today);
        fs.mkdirSync(currentDir);
    
        Object.values(folders['data']['area']).forEach(folder => {
            
            let dir = path.join(root, today, folder['_attributes']['name']);
            if (!fs.existsSync(dir)) {
                logger.info('[Mkdir] - creating folder ' + dir);
                fs.mkdirSync(dir);
            }
            else{
                logger.info('[Mkdir] - folder', dir, 'is existed');
            }
        });

        resolve(currentDir);
    });

    // console.log( Object.values(folders['data']['area']));
}

function queryString(subarea){
    return '?*[bbox='+ subarea['left'] + ',' + subarea['bottom'] + ',' + subarea['right'] + ',' +subarea['top'] + ']';
}

function pull(storageLocation, subarea) {
    logger.info('[Pull] - pulling subarea:', subarea)

    let filename = path.join(storageLocation, subarea['name'] + ".osm");
    let url_path = 'cgi/xapi_meta' + queryString(subarea);
    let url = buildUrl("http://overpass.openstreetmap.ru",{
        path: url_path,
    });
    
    const file = fs.createWriteStream(filename);
    const req = http.get(url, (res) => {
        res.pipe(file);
        logger.info('[Pull] -', filename, 'is pulled');
    });
}

function pulls(currentDir, data){
    return new Promise((resolve, reject) => {
        Object.values(data['data']['area']).forEach(area => {
            if (area['subarea'] != undefined){
                let storageLocation = path.join(currentDir, area['_attributes']['name']);
                Object.values(area['subarea']).forEach(subarea => {
                    pull(storageLocation, subarea['_attributes']);
                });
            }
        });
    });
}

function main() {
    fs.readFile('./data.xml', function (err, xml_data) {
        let data = convert.xml2json(xml_data, { compact: true, spaces: 4 });
        // console.log(data);
        mkdirs(JSON.parse(data)).then(currentDir => {
            pulls(currentDir, JSON.parse(data));
        });
    });

    // /*right format is 
    //     bbox=left, bottom, right, top

    //     eg: this example pull map data in Ho Chi Minh city
    //     https://api.openstreetmap.org/api/0.6/map?bbox=106.5543,10.6741,106.9032,10.8606
    // */
    // //https://api.openstreetmap.org/api/0.6/map?bbox=10.6741,106.9032,10.8606,106.5543
}

main();