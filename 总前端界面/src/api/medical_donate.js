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
  donateAPI(userId, amount, donationTime) {
    return axios
      .post('/api/donate', { userId, amount, donationTime })
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
  },
  //获取所有医生的姓名和id
  getDoctorsAPI() {
    return axios
      .get('/api/doctors')
      .then((response) => {
        // response.data 包含了从服务器返回的数据
        // 假设服务器返回的数据格式是数组，每个元素包含姓名和 ID
        return response.data.map(doctor => ({
          id: doctor.id,
          name: doctor.name
        }));
      })
      .catch(error => {
        throw new Error('获取医生数据时出错：' + error.message);
      });
    }

};
