# Pull map data
Tool for pulling data from [openstreet](https://www.openstreetmap.org/).

# How to use
Clone this repo, if it's the first time, we need to install all required modules via
```
npm install
```

Then run by 
```
npm start
```
## Provide data
Data pulled is depend on the data we give in `data.xml` file.

eg: This data is determine 2 area `Hanoi` and `HCMCity` thus 2 folder with perspective name will be created.
`HCMCity` has subarea which name is `district-2` and following coordinate, `district-2.osm` contain data will be pulled;
```xml
<data>
    <area name='Hanoi'/>
    <area name='HCMCity'>
        <subarea name='district-2' 
        left='162' bottom='10' right='163' top='11'/>
    </area>            
</data>
```

