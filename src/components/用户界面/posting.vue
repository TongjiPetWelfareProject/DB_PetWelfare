<template>
 <div class="postcontainer">

  <div class="post-form">
    <br>
    <div style="display: flex;align-items: center;">
      <img src="../../../public/heading.png" style="height:18px;width:18px"><span class="textpost" style="display: flex;align-items: center;justify-content: center;  ">&nbsp;标题</span>
    </div>
    <el-input v-model="input" placeholder="请输入标题"  style="margin-top:-8px" clearable />
    <div style="display: flex;align-items: center;">
      <img src="../../../public/内容.png" style="height:18px;width:18px"><span class="textpost" style="display: flex;align-items: center;justify-content: center;  ">&nbsp;内容</span>
    </div>
    <el-input
      v-model="textarea"
      :autosize="{ minRows: 24, maxRows: 30 }"
      type="textarea"
      placeholder="请输入内容"
      style="margin-top:-8px"
    />
    <el-upload action="#" list-type="picture-card" :auto-upload="false" :file-list="fileListRef">
      <el-icon><Plus /></el-icon>

      <template #file="{ file }">
        <div>
          <img class="el-upload-list__item-thumbnail" :src="file.url" alt="" />
          <span class="el-upload-list__item-actions">
            <span
              v-if="!disabled"
              class="el-upload-list__item-delete"
              @click="handleRemove(file)"
            >
              <el-icon><Delete /></el-icon>
            </span>
          </span>
        </div>
      </template>
    </el-upload>

    <el-dialog v-model="dialogVisible">
      <img w-full :src="dialogImageUrl" alt="Preview Image" />
    </el-dialog>
    <el-button type="primary" plain @click="submitPost">提交</el-button>
    <br>
  </div>
 </div>
 
</template>

<script lang="ts" setup>
import { ref } from 'vue'
import { Delete, Plus } from '@element-plus/icons-vue'
import type { UploadFile } from 'element-plus'
import posttocontent from '@/api/notice_forum'
import { useUserStore } from '@/store/user';
import { ElMessage, ElMessageBox } from 'element-plus'
import { useRouter } from 'vue-router'; // 请确保导入 useRouter

const input = ref('')
const textarea = ref('')
const fileListRef = ref<Array<UploadFile>>([]) // 创建fileList文件列表用于存储上传的照片
const userStore = useUserStore();
const router = useRouter();
const submitPost = async () => {
  try {
    const response = await posttocontent.postcontent(userStore.userInfo.User_ID, input.value, textarea.value);
    console.log('发帖成功', response);

    // 显示成功提示
    ElMessage.success({
      message: '发帖成功，帖子正在审核，审核通过后将展示在论坛界面中。',
      duration: 3000 // 持续显示时间（毫秒）
    });

    // 停顿3秒后跳转到 '/forum'
    setTimeout(() => {
      router.push('/forum');
    }, 3000);

  } catch (error) {
    console.error('发帖失败：', error);

    // 显示失败提示
    ElMessage.error({
      message: '发帖失败，错误信息：' + error.message,
      duration: 3000 // 持续显示时间（毫秒）
    });
  }
  
  input.value = '';
  textarea.value = '';
}

const dialogImageUrl = ref('')
const dialogVisible = ref(false)
const disabled = ref(false)

const handleRemove = (file: UploadFile) => {
  console.log('删除文件:', file)
  // 更新文件列表
  fileListRef.value = fileListRef.value.filter((item) => item.uid !== file.uid)
}

</script>

<style scoped>
.postcontainer{
  width: 100%;
  height:100%;
  /* background-color: rgb(244, 244, 244); */
  background-image: url("../../../public/postbg.png");
  background-size: 100% 100%;
  display: flex;
  justify-content: center;
  align-items: center;
  margin-top: -10px;
  border-radius: 20px;
  box-shadow: 0 0px 2px rgba(0, 0, 0, .2);
}
.post-form {
  display: flex;
  flex-direction: column;
  gap: 20px;
  width: 90vw;
  margin: 0 auto;
  /* padding: 20px; */
}

.textpost{
  font-size: 16px;
  color:#1e4579;
  margin-bottom: 0px;
}


</style>
