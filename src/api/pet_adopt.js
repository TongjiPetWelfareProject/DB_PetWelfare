// api.js
import axios from 'axios';

export const submitAdoptApplication = (userInfo, petInfo, formData) => {//export可以将对象暴露给其他模块使用
  const adoptData = {
    user: userInfo.User_ID,
    pet: petInfo.Pet_ID,
    gender: formData.gender,
    pet_exp: formData.pet_exp,
    long_term_care: formData.long_term_care,
    w_to_treat: formData.w_to_treat,
    d_care_h: formData.d_care_h,
    P_caregiver: formData.P_caregiver,
    f_popul: formData.f_popul,
    be_children: formData.be_children,
    accept_vis: formData.accept_vis,
  };

  return axios.post('/api/pet-adopt', adoptData)//指后端服务器的pet-adopt接口
    .then(response => {
      return response.data; // You might want to return the response data here
    })
    .catch(error => {
      throw error;
    });
};

export const getPetList = () => {
  return axios.get('/api/pet-list')
    .then(response => {
      return response.data.petList; // Adjust this according to your backend response structure
    })
    .catch(error => {
      throw error;
    });
};

export const getPetDetails = (petId) => {
  return axios.get(`/api/pet-details/${petId}`)
    .then(response => {
      return response.data; // Adjust this according to your backend response structure
    })
    .catch(error => {
      throw error;
    });
};
