// api.js
import axiosInstance from '@/util/token-config';
import axios from 'axios';

export const submitFosterApplication = (userInfo, formData, radioValue, sizeRadioValue) => {
  const fosterData = {
    user: userInfo.User_ID,
    name: formData.name,
    type: radioValue,
    size: sizeRadioValue,
    date: formData.date,
    num: formData.num,
    remark: formData.remark,
  };

  return axiosInstance.post('/api/pet-foster', fosterData)
    .then(response => {
      return response.data; // You might want to return the response data here
    })
    .catch(error => {
      throw error;
    });
};