/// @description 在此处插入描述 
// 你可以在此编辑器中写入代码 
draw_self()
draw_set_font(DefaultFont)
draw_set_color(DefaultShader)

//var font = DefaultFont.text_width; // 或者使用你自己的字体
var font_size=font_get_size(DefaultFont)
// 计算文字宽度
var text_width_pixels = string_width(text);
var text_width=text_width_pixels/font_size;
var screen_width = sprite_width*image_xscale; 
var text_x = (screen_width / 2) - (text_width / 2);
var text_y = sprite_height*image_yscale / 2;
show_debug_message(text_x);
show_debug_message(text_y);
draw_text(text_x,text_y,text)


//Room居中
//// 计算文字宽度
//var text_width = string_width(text);
//// 获取屏幕宽度（或你想让文字居中的区域宽度）
//var screen_width = room_width; // 如果你想让文字在整个房间宽度内居中
//// 计算文字的X坐标，使其水平居中
//var text_x = (screen_width / 2) - (text_width / 2);
//// 设置文字的Y坐标，这里我们简单地设置它为屏幕高度的一半
//var text_y = room_height / 2;