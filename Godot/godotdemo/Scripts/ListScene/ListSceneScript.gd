extends Node

var item=preload("res://Scene/ItemScene.tscn")
var list:VBoxContainer
var startId:int
  
func _ready() -> void:
	list=get_node("ScrollContainer/VBoxContainer")
	startId=5

func _on_add_item_button_pressed() -> void:
	var instance=item.instantiate()
	instance.Id=startId
	startId+=1
	instance.custom_minimum_size=Vector2(280, 128)
	# 这里可以设置texture、label、button
	list.add_child(instance)

func RemoveItemFromList(id:int):
	print(str(id))
	# 遍历item_container的所有子节点  
	for child in list.get_children():  
		# 检查子节点是否有Id属性，并且其值是否等于target_id  
		# if child.has_user_property("Id") and child.get("Id") == id: 
		if child.Id==id:
			# 从item_container中移除子节点  
			list.remove_child(child)
			# 你可以在这里添加额外的清理代码，比如释放资源等  
			# child.queue_free() # 通常不需要显式调用，除非你有特别的理由  
			return # 找到并删除后退出循环  
