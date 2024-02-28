import React, {useEffect, useImperativeHandle, useRef, useState, forwardRef} from 'react';
import mapboxgl from '!mapbox-gl'; // eslint-disable-line import/no-webpack-loader-syntax
import MapboxDraw from '@mapbox/mapbox-gl-draw';
import styles from '../App.module.scss';
import {putBoundaries} from '../services/api';
import { Snackbar } from '@mui/material';
import MuiAlert from '@mui/material/Alert';

const Alert = forwardRef(function Alert(props, ref) {
    return <MuiAlert elevation={6} ref={ref} variant="filled" {...props} />;
});

mapboxgl.accessToken =
    "pk.eyJ1IjoiaWduZWlzIiwiYSI6ImNsZ3o1bHI5ODBnNnkzcG9naXRrdnVvY2kifQ.tJlJbVUCrY2bsuuAGxQz8w";

const MapboxMap = forwardRef((props, ref) => {
    const mapContainer = useRef(null);
    const map = useRef(null);
    const draw = useRef(null);
    const marker = useRef(null);
    const [mode, setMode] = useState('simple_select');
    const [zoom, setZoom] = useState(7);
    const [latitude, setLatitude] = useState(55.2379);
    const [longitude, setLongitude] = useState(24.2219);
    const [boundaries, setBoundaries] = useState({
        id: 0,
        geoJson: '',
        placeId: 0
    });
    const [successMessageOpen, setSuccessMessageOpen] = useState(false);
    
    useImperativeHandle(ref, () => {
       return {
           drawEditablePolygon(boundaries, location) {
               if (!map.current) {
                   return;
               }
               
               if (marker.current) {
                   marker.current.remove();
               }

               map.current.flyTo({ center: new mapboxgl.LngLat(location.longitude, location.latitude), zoom: 12 });
               marker.current = new mapboxgl.Marker().setLngLat([location.longitude, location.latitude]).addTo(map.current);

               if (boundaries.geoJson === undefined) {
                   draw.current.deleteAll();
                   return;
               }
               
               setBoundaries(boundaries);

               const feature = {
                   id: 'place_boundary',
                   type: 'Feature',
                   properties: {},
                   geometry: JSON.parse(boundaries.geoJson)
               }

               draw.current.add(feature);
           }
       } 
    }, []);
    
    useEffect(() => {
        if (map.current) {
            return;
        }

        const mapOptions = {
            container: mapContainer.current,
            style: 'mapbox://styles/mapbox/streets-v12',
            center: new mapboxgl.LngLat(longitude, latitude),
            zoom: zoom,
        }
        
        const drawOptions = {
            displayControlsDefault: false,
            controls: {
                polygon: true,
                trash: true,
            }
        }
        
        draw.current = new MapboxDraw(drawOptions);
        map.current = new mapboxgl.Map(mapOptions);
        map.current.addControl(draw.current);
    });

    useEffect(() => {
        if (!map.current) {
            return;
        }

        map.current.on('move', () => {
            setLongitude(map.current.getCenter().lng);
            setLatitude(map.current.getCenter().lat);
            setZoom(map.current.getZoom().toFixed(2));
        });
        
        map.current.on('draw.modechange', () => {
            setMode(draw.current.getMode());
        });
        
        map.current.on('draw.create', updateBoundaries);
        map.current.on('draw.update', updateBoundaries);
        map.current.on('draw.delete', updateBoundaries);
    });
    
    const updateBoundaries = () => {
        const feature = draw.current.get('place_boundary');
        setBoundaries({ ...boundaries, geoJson: JSON.stringify(feature.geometry) });
    }
    
    const saveBoundaries = async () => {
        const response = await putBoundaries(boundaries);

        if (response.status) {
            setSuccessMessageOpen(true);
        }
        
        setTimeout(() => setSuccessMessageOpen(false), 2500);
    }
    
    return (
        <div className={styles.map}>
            <div className={styles.mapPosition}>
                Latitude: {latitude} | Longitude: {longitude} | Zoom: {zoom} | Mode: {mode} | <button className={styles.mapSaveButton} onClick={saveBoundaries}>Save</button>
            </div>
            <div ref={mapContainer} className={styles.mapContainer}/>
            <Snackbar open={successMessageOpen} anchorOrigin={{ vertical: 'top', horizontal: 'center' }}>
                <Alert severity="success">
                    Boundaries successfully saved
                </Alert>
            </Snackbar>
        </div>
    );
});

export default MapboxMap;
