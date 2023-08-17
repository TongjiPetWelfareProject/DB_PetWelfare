import axios from 'axios';

export default {
  //提交预约的api
  submitAppointmentAPI(formData) {
    return axios
      .post('/api/submitAppointment', formData)
      .then(response => response.data)
      .catch(error => {
        throw new Error('提交预约失败：' + error.message);
      });
  },
//捐款的api
  donateAPI(amount) {
    return axios
      .post('/api/donate', { amount })
      .then(response => response.data)
      .catch(error => {
        throw new Error('捐款失败：' + error.message);
      });
  },
//获取三个医生的照片
  doctorsAPI() {
    return axios
      .get('/api/doctors')
      .then(response => response.data)
      .catch(error => {
        throw new Error('获取医生数据时出错：' + error.message);
      });
  },
  //获取五个康复故事
  recoveryStoryAPI() {
    return axios
      .get('/api/recoveryStory')
      .then(response => response.data)
      .catch(error => {
        throw new Error('获取康复故事数据时出错：' + error.message);
      });
  },
  //获取捐助记录,时间、用户、金额
  donationRecordsAPI() {
    return axios
    .get('/api/donation')
      .then((response) => response.data)
      .catch(error => {
        throw new Error('获取捐助记录数据时出错：' + error.message);
      });
  }
};
