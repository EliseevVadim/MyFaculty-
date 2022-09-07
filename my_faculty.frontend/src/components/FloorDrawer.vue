<template>
	<div>
		<div id="drawing-area">
			<svg id="drawing-container">

			</svg>
		</div>
		<v-row>
			<v-col xs="6">
				<v-btn
					v-ripple
					@click="clearDrawingArea"
					color="red"
					dark
				>
					Очистить
				</v-btn>
			</v-col>
			<v-col xs="6">
				<v-btn
					v-ripple
					@click="openEditing"
					color="primary"
					dark
					right
				>
					Редактировать
				</v-btn>
			</v-col>
		</v-row>
		<div v-if="displayEditingTool && result.length > 0">
			<h2 class="text-center">Изменить координаты нарисованных точек</h2>
			<v-row>
				<v-col cols="3" md="4" class="mt-3" v-for="(point, index) in this.result" :key="index">
					<v-text-field
						label="X-координата*"
						required
						:rules="numberRules"
						hide-details="auto"
						type="number"
						v-model.number="point[0]"
					></v-text-field>
					<v-text-field
						label="Y-координата*"
						required
						:rules="numberRules"
						hide-details="auto"
						type="number"
						v-model.number="point[1]"
					></v-text-field>
				</v-col>
				<v-row style="width: 100%;">
					<v-col xs="6">
						<v-btn
							v-ripple
							@click="displayEditingTool = false"
							color="red"
							dark
						>
							Закрыть
						</v-btn>
					</v-col>
					<v-col xs="6">
						<v-btn
							v-ripple
							@click="updateCoordinates"
							color="success"
							dark
							right
						>
							Применить
						</v-btn>
					</v-col>
				</v-row>
			</v-row>
		</div>
	</div>
</template>

<script>
export default {
	name: "FloorDrawer",
	props: ['floor'],
	data() {
		return {
			dragging: false,
			drawing: false,
			startPoint: null,
			points: [],
			result: this.floor.bounds,
			g: null,
			svg: null,
			displayEditingTool: false,
			numberRules: [
				v => !!v || 'Поле является обязательным для заполнения',
				v => Number.isInteger(Number(v)) || "Значение должно быть целым числом"
			],
		}
	},
	mounted() {
		this.svg = this.$d3.select('#drawing-container');
		this.svg.on('mouseup', this.drawingAreaMouseUp);
		this.svg.on('mousemove', this.drawingAreaMouseMove);
		if (this.floor.bounds !== null) {
			this.closePolygon(this.floor.bounds);
			this.result = this.floor.bounds;
		}
	},
	methods: {
		clearDrawingArea() {
			this.$d3.selectAll('g').remove();
			this.points.splice(0);
			this.result.splice(0);
		},
		drawingAreaMouseUp() {
			let svg = this.$d3.select('#drawing-container');
			if (this.dragging)
				return;
			this.drawing = true;
			let x = this.calculateMouseXCoordinate();
			let y = this.calculateMouseYCoordinate();
			this.startPoint = [x, y];
			if(svg.select('g.drawPoly').empty())
				this.g = svg.append('g').attr('class', 'drawPoly');
			if(event.target.hasAttribute('is-handle')) {
				this.closePolygon(this.points);
				return;
			}
			this.points.push(this.startPoint);
			this.g.select('polyline').remove();
			let polyline = this.g.append('polyline').attr('points', this.points)
				.style('fill', 'none')
				.attr('stroke', '#000');
			for(let i = 0; i < this.points.length; i++) {
				this.g.append('circle')
					.attr('cx', this.points[i][0])
					.attr('cy', this.points[i][1])
					.attr('r', 4)
					.attr('fill', 'yellow')
					.attr('stroke', '#000')
					.attr('is-handle', 'true')
					.style({cursor: 'pointer'});
			}
		},
		drawingAreaMouseMove() {
			if(!this.drawing) return;
			let g = this.$d3.select('g.drawPoly');
			g.select('line').remove();
			let line = g.append('line')
				.attr('x1', this.startPoint[0])
				.attr('y1', this.startPoint[1])
				.attr('x2', this.calculateMouseXCoordinate() + 2)
				.attr('y2', this.calculateMouseYCoordinate())
				.attr('stroke', '#53DBF3')
				.attr('stroke-width', 1);
		},
		closePolygon(data) {
			let svg = this.$d3.select('#drawing-container');
			svg.select('g.drawPoly').remove();
			let g = svg.append('g');
			g.append('polygon')
				.attr('points', data)
				.style('fill', 'none')
				.style("stroke", "black")
				.style("stroke-width", "2");
			for(let i = 0; i < data.length; i++) {
				let circle = g.selectAll('circles')
					.data([data[i]])
					.enter()
					.append('circle')
					.attr('cx', data[i][0])
					.attr('cy', data[i][1])
					.attr('r', 4)
					.attr('fill', '#FDBC07')
					.attr('stroke', '#000')
					.attr('is-handle', 'true')
					.style({cursor: 'move'})
			}
			this.result = this.points.slice();
			this.points.splice(0);
			this.drawing = false;
		},
		calculateMouseXCoordinate() {
			return event.pageX - document.getElementById('drawing-container').getBoundingClientRect().x;
		},
		calculateMouseYCoordinate() {
			return event.pageY - document.getElementById('drawing-container').getBoundingClientRect().y;
		},
		updateCoordinates() {
			this.$d3.selectAll('g').remove();
			let temp = this.result.slice();
			this.closePolygon(this.result);
			this.result = temp.slice();
		},
		openEditing() {
			if (this.floor.bounds !== null)
				this.result = this.floor.bounds;
			this.displayEditingTool = true;
		}
	}
}
</script>

<style scoped>
	#drawing-area {
		margin: 10px auto;
		border: 1px solid black;
		width: 851px;
		height: 606px;
		overflow: hidden;
	}
	#drawing-container {
		height: 100%;
		width: 100%;
		touch-action: none;
		overflow: hidden;
	}
</style>