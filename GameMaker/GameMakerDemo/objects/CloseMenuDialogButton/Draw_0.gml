/// @description 在此处插入描述 
// 你可以在此编辑器中写入代码 
draw_self()
draw_set_font(DefaultFont)
draw_set_color(DefaultShader)

//Sprite set as left-top
//var font = DefaultFont.text_width;
var font_size=font_get_size(DefaultFont)
// get font width
var text_width_pixels = string_width(text);
var text_width=text_width_pixels/font_size;
var screen_width = sprite_width*image_xscale; 
//var screen_width = 794*image_xscale; 
var text_x = x+ (screen_width - text_width);
var text_y = y+sprite_height*image_yscale;
//var text_y = 268*image_yscale / 2;
show_debug_message(text_x);
show_debug_message(text_y);
draw_text(text_x,text_y,text)


//Center in Room
//// get font width
//var text_width = string_width(text);
//// get screen width
//var screen_width = room_width;
//// horizontal centering
//var text_x = (screen_width / 2) - (text_width / 2);
//// vehicle centering
//var text_y = room_height / 2;