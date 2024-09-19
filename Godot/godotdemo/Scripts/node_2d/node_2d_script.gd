extends Node2D
var head : PackedVector2Array
var mouth : PackedVector2Array
var _mouth_width : float = 4.4
var coords_mouth = [
	[ 22.817, 81.100 ], [ 38.522, 82.740 ],
	[ 39.001, 90.887 ], [ 54.465, 92.204 ],
	[ 55.641, 84.260 ], [ 72.418, 84.177 ],
	[ 73.629, 92.158 ], [ 88.895, 90.923 ],
	[ 89.556, 82.673 ], [ 105.005, 81.100 ]
]
var coords_head : Array = [
	[ 22.952, 83.271 ],  [ 28.385, 98.623 ],
	[ 53.168, 107.647 ], [ 72.998, 107.647 ],
	[ 99.546, 98.623 ],  [ 105.048, 83.271 ],
	[ 105.029, 55.237 ], [ 110.740, 47.082 ],
	[ 102.364, 36.104 ], [ 94.050, 40.940 ],
	[ 85.189, 34.445 ],  [ 85.963, 24.194 ],
	[ 73.507, 19.930 ],  [ 68.883, 28.936 ],
	[ 59.118, 28.936 ],  [ 54.494, 19.930 ],
	[ 42.039, 24.194 ],  [ 42.814, 34.445 ],
	[ 33.951, 40.940 ],  [ 25.637, 36.104 ],
	[ 17.262, 47.082 ],  [ 22.973, 55.237 ]
]

func _ready() -> void:
	#$Parent.add_child(Node.new())
	#get_parent().set("visible", false)
	#$Parent.a()
	head = float_array_to_Vector2Array(coords_head);
	mouth = float_array_to_Vector2Array(coords_mouth);
	#var path=""
	#AudioStreamOggVorbis.load_from_file(path)
	#var image = Image.load_from_file(path)
	var text1=tr("key")
	var text2=tr("%s picked up the %s") % ["Ogre", "Sword"]
	var text3=tr("{character} picked up the {weapon}").format({character = "Ogre", weapon = "Sword"})
	pass # Replace with function body.
	
func float_array_to_Vector2Array(coords : Array) -> PackedVector2Array:
	# Convert the array of floats into a PackedVector2Array.
	var array : PackedVector2Array = []
	for coord in coords:
		array.append(Vector2(coord[0], coord[1]))
	return array

func _draw():
	var white : Color = Color.WHITE
	var godot_blue : Color = Color("478cbf")
	var grey : Color = Color("414042")

	draw_polygon(head, [ godot_blue ])
	draw_polyline(mouth, white, _mouth_width)

	# Four circles for the 2 eyes: 2 white, 2 grey.
	draw_circle(Vector2(42.479, 65.4825), 9.3905, white)
	draw_circle(Vector2(85.524, 65.4825), 9.3905, white)
	draw_circle(Vector2(43.423, 65.92), 6.246, grey)
	draw_circle(Vector2(84.626, 66.008), 6.246, grey)

func loadFont():
	var path = "/path/to/font.ttf"
	var path_lower = path.to_lower()
	var font_file = FontFile.new()
	if (
			path_lower.ends_with(".ttf")
			or path_lower.ends_with(".otf")
			or path_lower.ends_with(".woff")
			or path_lower.ends_with(".woff2")
			or path_lower.ends_with(".pfb")
			or path_lower.ends_with(".pfm")
	):
		font_file.load_dynamic_font(path)
	elif path_lower.ends_with(".fnt") or path_lower.ends_with(".font"):
		font_file.load_bitmap_font(path)
	else:
		push_error("Invalid font file format.")

	if not font_file.data.is_empty():
		# If font was loaded successfully, add it as a theme override.
		$Label.add_theme_font_override("font", font_file)
	
