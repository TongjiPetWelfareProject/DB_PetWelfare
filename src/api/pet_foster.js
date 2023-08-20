// 寄养申请表接口
import axios from 'axios';

const submitFosterApplication = ({user, name, type, size, date, num, remark}) => {
    const fosterData = {
      user,
      name,
      type,
      size,
      date,
      num,
      remark,
    };
  return axios.post('/api/petfoster', fosterData)
    .then(response => {
      console.log("发送成功")
      return response.data;
    })
    .catch(error => {
      console.log("发送失败")
      throw error;
    });
}

export default submitFosterApplication;
