// api.js
import axiosInstance from '@/util/token-config';
import axios from 'axios';

export const fetchAdoptionRecords = async () => {
  try {
    const response = await axiosInstance.get('/api/manage-adopt');
    return response.data;
  } catch (error) {
    throw error;
  }
};

export const updateAdoptionRecord = async (record) => {
  try {
    await axiosInstance.patch(`/api/manage-adopt-update`, record);
  } catch (error) {
    throw error;
  }
};

export const fetchFosterRecords = async () => {
    try {
      const response = await axiosInstance.get('/api/manage-foster');
      return response.data;
    } catch (error) {
      throw error;
    }
  };
  
  export const updateFosterRecord = async (record) => {
    try {
      await axiosInstance.patch(`/api/manage-foster-update`, record);
    } catch (error) {
      throw error;
    }
  };