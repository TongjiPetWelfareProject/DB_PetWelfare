<template>
    <div class="blog-editor">
      <el-form ref="form" :model="blog" label-width="40px">
        <el-form-item label="标题">
          <el-input
            :rows="1"
            type="textarea"
            placeholder="请输入标题"
            v-model="blog.title"
          ></el-input>
        </el-form-item>
  
        <el-form-item label="摘要">
          <el-input
            :rows="3"
            type="textarea"
            placeholder="请输入摘要，不超过100字"
            v-model="blog.description"
          ></el-input>
        </el-form-item>
  
        <el-form-item label="分类">
          <el-radio-group v-model="blog.type" size="default">
            <el-radio-button label="原创" />
            <el-radio-button label="翻译" />
            <el-radio-button label="转载" />
          </el-radio-group>
        </el-form-item>
  
        <el-form-item label="内容">
          <v-md-editor
            ref="editor"
            v-model="blog.content"
            height="450px"
          ></v-md-editor>
        </el-form-item>
  
        <el-form-item style="margin-left: 44%">
          <el-button type="primary" round @click="submitBlog">发布</el-button>
        </el-form-item>
      </el-form>
    </div>
  </template>
    
  <script setup>
  import DataService from "@/components/services/DataService";
  import { ref } from "vue";
  import { defineProps } from "vue";
  const params = defineProps({
    blog_id: {
      type: Number,
      default: -1,
    },
    user_id: {
      type: Number,
      required: true,
    },
    title: {
      type: String,
      default: "",
    },
    description: {
      type: String,
      default: "",
    },
    content: {
      type: String,
      default: "",
    },
    type: {
      type: String,
      default: "原创",
    },
    onClose: Function,
  });
  const blog = ref({
    title: params.title,
    description: params.description,
    content: params.content,
    type: params.type,
  });
  const submitBlog = async () => {
    console.log(blog.value);
    await submit();
    console.log("submit ok");
    // if (params.blog_id === -1) {
    //   router.push({path:'/blog',query:{content:''}})
    // }else{
    //   router.push({name:'BlogPresent',params: {blogId:params.blog_id}})
    // }
    params.onClose();
    // 将博客内容转换为Markdown格式存储
    // 提交博客数据到后台
    // ...
  };
  const submit = async () => {
    if (params.blog_id === -1) {
      const responce = await DataService.insertBlog(
        params.user_id,
        blog.value.type,
        blog.value.description,
        blog.value.title,
        blog.value.content
      );
      console.log(responce.data);
    } else {
      if (blog.value.content !== params.content) {
        const responce = await DataService.Update_blog_Content(
          params.blog_id,
          params.user_id,
          blog.value.content
        );
        console.log(responce.data);
      }
      if (blog.value.title !== params.title) {
        const responce = await DataService.Update_blog_Title(
          params.blog_id,
          params.user_id,
          blog.value.title
        );
        console.log(responce.data);
      }
      if (blog.value.description !== params.description) {
        const responce = await DataService.Update_blog_Description(
          params.blog_id,
          params.user_id,
          blog.value.description
        );
        console.log(responce.data);
      }
      if (blog.value.type !== params.type) {
        const responce = await DataService.Update_blog_Type(
          params.blog_id,
          params.user_id,
          blog.value.type
        );
        console.log(responce.data);
      }
    }
  };
  </script>
  
    
  <style scoped>
  .blog-editor {
    width: 100%;
    height: 90%;
    margin: 0 auto;
  }
  .el-form {
    width: 100%;
    height: 100%;
    margin-left: 0px;
    margin-top: 0px;
    border-color: #e6e6e6;
  }
  </style>
    