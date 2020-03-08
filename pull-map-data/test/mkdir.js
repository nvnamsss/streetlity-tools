const assert = require('assert');
const fs = require('fs');
const convert = require('xml-js');
const path = require('path');

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
                fs.mkdirSync(dir);
            }
        });

        resolve(currentDir);
    });

    // console.log( Object.values(folders['data']['area']));
}

function test() {
    describe('Make dir', function () {
        describe('Ensure that folders are created and areas perspective', function () {
            it('', function () {
                fs.readFile('./data.xml', function (err, xml_data) {
                    let data = JSON.parse(convert.xml2json(xml_data, { compact: true, spaces: 4 }));
                    mkdirs(data).then(currentDir => {

                        Object.values(data['data']['area']).forEach(folder => {
                            let dir = path.join(currentDir, folder['_attributes']['name']);
                            assert.equal(fs.existsSync(dir), true);
                        });

                    });
                });

            });
        });
    });
}

module.exports = {
    test
}