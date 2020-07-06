const axios = require('axios').default;
const apikey = 'AIzaSyB56CeF7ccQ9ZeMn0O4QkwlAQVX7K97-Ss';
const url = 'https://maps.googleapis.com/maps/api/geocode/json';


async function reverseRawText(text) {

}

async function reverse(lat, lon) {
    try{
        let res = await axios.get(url, {
            params: {
                "latlng": lat + ',' + lon,
                key : apikey
            }
        });
    
        console.log(res)
        console.log(res.data.results[1].formatted_address)
        return res.data.results[1].formatted_address;
    } catch (error){
        console.log(error);
    }

    return "";
}

module.exports = {
    reverse
}