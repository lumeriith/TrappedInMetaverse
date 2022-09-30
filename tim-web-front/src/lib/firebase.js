import { initializeApp } from "firebase/app";
import { getAuth, onAuthStateChanged, signInAnonymously } from "firebase/auth";
import { writable } from "svelte/store";
import { getRandomName } from "./flowernames";

const firebaseConfig = {
  apiKey: "AIzaSyBo3Bw3o1KI-5jn4yXViPXNUs-S47hf99w",
  authDomain: "trappedinmetaverse.firebaseapp.com",
  databaseURL: "https://trappedinmetaverse.firebaseio.com",
  projectId: "trappedinmetaverse",
};

export const app = initializeApp(firebaseConfig);
export const isLoggedIn = writable(false);
export const userName = getRandomName();

const auth = getAuth();
signInAnonymously(auth);

onAuthStateChanged(auth, (user) => {
  isLoggedIn.set(!!user);
});
