<template>
  <div class="post-form">
    <p>帖子标题</p>
    <el-input v-model="input" placeholder="输入标题" clearable />
    <p>帖子内容</p>
    <el-input
      v-model="textarea"
      :autosize="{ minRows: 2, maxRows: 4 }"
      type="textarea"
      placeholder="输入内容"
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
.post-form {
  display: flex;
  flex-direction: column;
  gap: 20px;
  max-width: 400px;
  margin: 0 auto;
  padding: 20px;
}
</style>

