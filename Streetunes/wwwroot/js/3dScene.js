//import * as THREE from 'three';
//import { GLTFLoader } from '/three/examples/jsm/loaders/GLTFLoader.js';  // Import GLTFLoader
//import * as THREE from '/three/build/three.module.js';
import * as THREE from 'https://cdn.jsdelivr.net/npm/three@0.136.0/build/three.module.js';
//import { GLTFLoader } from 'https://cdn.jsdelivr.net/npm/three@0.136.0/examples/jsm/loaders/GLTFLoader.js';


// Select the canvas
const canvas = document.getElementById('threeCanvas');

// Create the scene, camera, and renderer
const scene = new THREE.Scene();
const camera = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 0.1, 1000);
const renderer = new THREE.WebGLRenderer({ canvas, alpha: true });
renderer.setSize(window.innerWidth, window.innerHeight);

// Add a light to the scene
const light = new THREE.PointLight(0xffffff, 1);
light.position.set(0, 15, 0);
scene.add(light);

const ambientLight = new THREE.AmbientLight(0xffffff); // Optional, soft light
scene.add(ambientLight);

// Load the GLTF model
/*const loader = new GLTFLoader();
let model;

// Assuming you have your GLTF file stored locally as 'model.glb'
loader.load('/3dModels/bender_rodriguez.glb', (gltf) => {
    model = gltf.scene;  // Get the scene from the loaded GLTF
    scene.add(model);    // Add the model to the scene
    model.position.set(0, 0, 0);  // Adjust position if needed
});*/

// Create a football-like sphere
const geometry = new THREE.SphereGeometry(2, 32, 32);
const textureLoader = new THREE.TextureLoader();
const material = new THREE.MeshBasicMaterial({ color: 0x00ff00 });

const football = new THREE.Mesh(geometry, material);
scene.add(football);

// Position the camera
camera.position.z = 7;

// Animation loop
function animate() {
    requestAnimationFrame(animate);

   // if (model) {
  //      model.rotation.y += 0.01; // Rotate the model if it has been loaded
   //     model.rotation.x += 0.02; // Rotate the model
    // }

    
    football.rotation.y += 0.01
    football.rotation.x += 0.02; // Rotate the sphere

    renderer.render(scene, camera);
}
animate();

// Adjust canvas on window resize
window.addEventListener('resize', () => {
    camera.aspect = window.innerWidth / window.innerHeight;
    camera.updateProjectionMatrix();
    renderer.setSize(window.innerWidth, window.innerHeight);
});
