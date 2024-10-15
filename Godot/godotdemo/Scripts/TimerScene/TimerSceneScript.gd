extends Node

var time_label :Label
var timer :Timer
var current_time = 0 # 当前时间（毫秒）

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	time_label=get_node("Label")
	timer=get_node("Timer")

func _update_time():
	# 更新时间
	current_time += 1
	update_time_label(current_time)

func update_time_label(time_in_ms):
	# 格式化时间为时:分:秒
	var hours = floor(time_in_ms / 3600)
	var minutes = floor((time_in_ms % 3600) / 60)
	var seconds = floor(time_in_ms % 60)

	# 格式化输出
	var h="%02d" % hours
	var m="%02d" % minutes
	var s="%02d" % seconds
	var time_str = h+":"+m+":"+s
	time_label.text = time_str
