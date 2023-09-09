<template>
 <div class="postcontainer">

  <div class="post-form">
    <div style="display: flex;align-items: center;margin-top:10px">
      <img src=" ../../../public/return.png" class="textreturn" style="width:24px;height: 24px;">
              &nbsp;<a href="\forum" style="text-decoration: none;color:#538adc;">返回论坛</a>
        </div>
    <div style="display: flex;align-items: center;">
      <img src="../../../public/heading.png" style="height:18px;width:18px"><span class="textpost" style="display: flex;align-items: center;justify-content: center;font-weight:bold  ">&nbsp;标题</span>
    </div>
    <el-input v-model="input" placeholder="请输入标题"  style=" box-shadow: 0 0px 1px rgba(0, 0, 0, .2);font-weight:bold;margin-top:-8px;font-size:19px;height:45px" clearable />
    <div style="display: flex;align-items: center;">
      <img src="../../../public/内容.png" style="height:18px;width:18px"><span class="textpost" style="display: flex;align-items: center;justify-content: center; font-weight:bold ">&nbsp;内容</span>
    </div>
    <el-input
      v-model="textarea"
      :autosize="{ minRows: 24, maxRows: 30 }"
      type="textarea"
      placeholder="请输入正文"
      style="margin-top:-8px;font-size:15px;box-shadow: 0 0px 1px rgba(0, 0, 0, .2);"
    />
    <div style="display: flex;align-items: center;">
      <el-icon src="../../../public/内容.png" style="height:20px;width:20px"><Upload/></el-icon><span class="textpost" style="display: flex;align-items: center;justify-content: center; font-weight:bold ">&nbsp;上传图片(可选)</span>
    </div>
    <el-upload
            :http-request="httpRequest"
            multiple
            :show-file-list="true"
            list-type="picture-card"
    ><el-icon><Plus /></el-icon>
    </el-upload>

    <el-button type="primary" plain @click="submitPost">发布</el-button>
    <br>
  </div>
 </div>
 
</template>

<script setup>
import { ref } from 'vue'
import { Delete, Plus,Upload } from '@element-plus/icons-vue'
// import type { UploadFile } from 'element-plus'
import posttocontent from '@/api/notice_forum'
import { useUserStore } from '@/store/user'
import { ElMessage } from 'element-plus'
import { useRouter } from 'vue-router'
import axios from "axios";


const input = ref('')
const textarea = ref('')

const fileList = ref([])

const userStore = useUserStore();
const router = useRouter();

const fileUpload = ref(null)

// 设置请求头
const headers = {
  'Content-Type': 'multipart/form-data'
}

const submitPost = async () => {
  if (!input.value || !textarea.value) {
    ElMessage.warning('标题或内容不可为空');
    return;
  }
  try {
    let param = new FormData()

    param.append('user_id', userStore.userInfo.User_ID)
    param.append('post_title', input.value)
    param.append('post_content', textarea.value)

    fileList.value.forEach((it,index)=>{
        param.append('filename',it.file)
    })

    await axios({
        method: 'POST',
        url: '/api/postcontent',
        data: param,
        headers: {
            "Content-Type": "multipart/form-data"
        }
    }).then(response => {
        console.log(response.data)
    })


    // 显示成功提示
    ElMessage.success({
      message: '发帖成功，帖子正在审核，审核通过后将展示在论坛界面中。',
      duration: 2000 // 持续显示时间（毫秒）
    });

    // 停顿3秒后跳转到 '/forum'
    setTimeout(() => {
      router.push('/forum');
    }, 2000);

  } catch (error) {
    console.error('发帖失败：', error);

    // 显示失败提示
    ElMessage.error({
      message: '发帖失败，您已被禁言，请等待解禁',
      duration: 3000 // 持续显示时间（毫秒）
    });
  }
}


function httpRequest(option) {
    fileList.value.push(option)
}



const dialogImageUrl = ref('')
const dialogVisible = ref(false)
const disabled = ref(false)

// 选择文件时被调用，将他赋值给fileUpload
// const handleChange = (file: any) => {
//   fileList.value.push(file)
// }

// const handleRemove = (file: UploadFile) => {
//   console.log('删除文件:', file)
//   fileUpload.value = fileUpload.value.filter((item) => item.uid !== file.uid)
// }

</script>

<style scoped>
.postcontainer{
  width: 100%;
  height:100%;
  /* background-color: rgb(244, 244, 244); */
  /* background-image: url("../../../public/postbg.png"); */
  background-color: rgb(241, 246, 250);
  background-size: 100% 100%;
  display: flex;
  justify-content: center;
  align-items: center;
  margin-top: -10px;
  border-radius: 20px;
  box-shadow: 0 0px 4px rgba(0, 0, 0, .2);
}
.post-form {
  display: flex;
  flex-direction: column;
  gap: 20px;
  width: 60vw;
  margin: 0 auto;
  /* padding: 20px; */
}

.textpost{
  font-size: 16px;
  color:#1e4579;
  margin-bottom: 0px;
}


</style>
