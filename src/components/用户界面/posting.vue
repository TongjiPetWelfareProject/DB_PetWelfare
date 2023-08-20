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
    <!-- <el-upload action="#" list-type="picture-card" :auto-upload="false" :file-list="fileListRef">
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
    </el-upload> -->

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

const input = ref('')
const textarea = ref('')
const fileListRef = ref<Array<UploadFile>>([]) // 创建fileList文件列表用于存储上传的照片

const submitPost = () => {
  // 处理提交逻辑，可以在这里调用接口或进行其他操作
  console.log('提交帖子:', input.value, textarea.value)

  // 提交后清空输入框
  input.value = ''
  textarea.value = ''
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
