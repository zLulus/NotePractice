/// @description 在此处插入描述 
// 你可以在此编辑器中写入代码 
// 获取屏幕中心位置
//***动态创建实例对象
var center_x = room_width / 2;
var center_y = room_height / 2;

// 创建文本显示实例
var inst = instance_create_layer(center_x, center_y,"Instances", Text,
{
	//可以在这里直接给Object的text及其其他属性赋值
	//可以在Draw事件中赋值
	//可以给inst的属性赋值
	text:"Hello world"
});

// 设置实例的参数
//inst.text = "Hello, World!";
//inst.font = font_SpecialFont; // 假设你已经创建了一个名为 font_SpecialFont 的字体
//inst.color = c_red;
//inst.scale = 2.0;

