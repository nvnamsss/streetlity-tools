var express = require('express');
var router = express.Router();
const geocoding = require('../geocoding/reverse');

router.get('/reverse', async function (req, res) {
    let lat = req.query["lat"];
    let lon = req.query["lon"];
    if (lat == undefined) {
        res.status(200).send("");
        return;
    }

    if (lon == undefined) {
        res.status(200).send("");
        return;
    }

    let result = await geocoding.reverse(lat, lon);
    res.status(200).send(result);
});

module.exports = router;