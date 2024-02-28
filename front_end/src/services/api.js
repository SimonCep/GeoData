import axios from "axios";

const baseUrl = "https://geodataproject.azurewebsites.net/api";

const defaultModels = {
  name: {
    id: 0,
    locale: "",
    value: "",
    placeId: 0,
  },
  place: {
    id: 0,
    population: 0,
    rating: 0,
    hierarchy: "",
  },
};

// Tags
export const getTagsByPlaceId = async (placeId) => {
  const response = await axios.get(`${baseUrl}/tags?placeId=${placeId}`);
  return response.data;
};

// Location
export const getLocationByPlaceId = async (placeId) => {
  const response = await axios.get(`${baseUrl}/locations/${placeId}`);
  return response.data;
};

// Hyierarchy
export const getHierarchyByPlaceId = async (placeId) => {
  const response = await axios.get(`${baseUrl}/places/${placeId}`);

  const ids = response.data.hierarchy.split(','); // Split the hierarchy string into an array of IDs

  const hierarchyList = [];

  for (const id of ids) {
    const data = await getNamesByPLaceIdAndLocale(parseInt(id.trim()), "en");
    hierarchyList.push(data.value);
  }

  return hierarchyList;
};

// Boundaries
export const getBoundariesByPlaceId = async (placeId) => {
  const response = await axios.get(`${baseUrl}/boundaries/${placeId}`);
  return response.data;
};

export const putBoundaries = async (boundaries) => {
  return await axios.put(`${baseUrl}/boundaries/${boundaries.id}`, boundaries);
};

// Names
export const getNamesByPlaceId = async (placeId) => {
  const response = await axios.get(`${baseUrl}/Names?placeId=${placeId}`);
  if (response.status === 200) {
    return response.data;
  }
  return defaultModels.name;
};

export const getNamesByPartialEntry = async (value, locale) => {
  const response = await axios.get(`${baseUrl}/names/${value}&${locale}`);
  return response.data;
};

export const getNamesByPLaceIdAndLocale = async (placeId, locale) => {
  const response = await axios.get(`${baseUrl}/Names/${placeId}/${locale}`);
  return response.data;
};

export const getNameByValue = async (value) => {
  const response = await axios.get(`${baseUrl}/names/${value}`);

  if (response.status === 200) {
    return response.data;
  }

  // Generally it is recommended to return some kind of default value to avoid null reference exceptions and not break the application flow
  return defaultModels.name;
};

// Places
export const getPlaceById = async (id) => {
  const response = await axios.get(`${baseUrl}/places/${id}`);

  if (response.status === 200) {
    return response.data;
  }

  return defaultModels.place;
};

export const postNameByPlaceId = async (nameData) => {
  try {
    const response = await axios.post(
      "https://geodataproject.azurewebsites.net/api/Names",
      nameData
    );
    if (response.status === 200) {
      console.log("Place created successfully");
    } else {
      console.log("Error creating place:", response.status);
    }
  } catch (error) {
    console.error("Error creating place:", error);
  }
};

export const deleteNameByPlaceId = async (nameId) => {
  try {
    const response = await axios.delete(
      `https://geodataproject.azurewebsites.net/api/Names/${nameId}`
    );
    if (response.status === 200) {
      console.log("Name deleted successfully");
    } else {
      console.log("Error deleting name:", response.status);
    }
  } catch (error) {
    console.error("Error deleting name:", error);
  }
};

export const putNameByPlaceId = async (nameId, nameData) => {
  try {
    const response = await axios.put(
      `https://geodataproject.azurewebsites.net/api/Names/${nameId}`,
      nameData
    );
    if (response.status === 200) {
      console.log("Name updated successfully");
    } else {
      console.log("Error updating name:", response.status);
    }
  } catch (error) {
    console.error("Error updating name:", error);
  }
};

// Authentication
export const login = async (email, password) => {
  const response = await axios.post(`${baseUrl}/Authentication/login`, { email, password });
  
  if (response.status === 200)
  {
    return response.data.token;
  }
  
  return '';
}

export const register = async (email, password) => {
  const response = await axios.post(`${baseUrl}/Authentication/register`, { email, password });

  if (response.status === 200)
  {
    return true;
  }

  return false;
}