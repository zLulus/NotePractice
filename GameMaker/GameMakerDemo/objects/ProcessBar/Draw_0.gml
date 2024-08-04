/// @description 在此处插入描述 
// 你可以在此编辑器中写入代码 
// Draw event
// 绘制背景
//draw_sprite_ext(spr_Progress_Bar_Background, 0, x, y, 1, 1, 0, c_white, 1);

// 计算填充宽度
var full_width= sprite_width*image_xscale;
var fill_width = (current_progress / max_progress) *full_width;

// 绘制填充部分
draw_sprite_ext(BlueProcessBarSprite, 0, x, y, fill_width / full_width, 1, 0, c_white, 1);