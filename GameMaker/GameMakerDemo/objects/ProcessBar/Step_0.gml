/// @description 在此处插入描述 
// 你可以在此编辑器中写入代码 
// Step event
//if (keyboard_check(vk_space)) { // 按下空格键时增加进度
    current_progress += 1;
    if (current_progress > max_progress) {
        current_progress = max_progress;
    }
//}

// 重置进度
if (keyboard_check(vk_enter)) { // 按下 R 键时重置进度
    current_progress = 0;
}