import axios from 'axios';

export default {
  submitAppointmentAPI(formData) {
    return axios
      .post('/api/submitAppointment', formData)
      .then(response => response.data)
      .catch(error => {
        throw new Error('提交预约失败：' + error.message);
      });
  },

  donateAPI(amount) {
    return axios
      .post('/api/donate', { amount })
      .then(response => response.data)
      .catch(error => {
        throw new Error('捐款失败：' + error.message);
      });
  },
};
