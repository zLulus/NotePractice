extends MenuButton
class_name CustomMenuButton

func _ready():
	# 连接 popup 关信号
	connect("popup", _on_popup)

func _on_popup():
	# todo 没有走到这里
	# 获取菜单
	var menu = get_popup()
	
	# 计算菜单的新位置
	var position = get_global_rect().position
	var size = get_global_rect().size
	var menu_size = menu.get_size()
	
	# 计算新的 y 位置，使菜单向上展开
	var new_y = position.y - menu_size.y
	
	# 设置菜单的新位置
	menu.popup(Rect2(position.x, new_y, menu_size.x, menu_size.y))
