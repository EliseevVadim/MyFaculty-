<template>
    <div>
        <div v-if="!userAuthorized" id="screwOffContainer" @mousemove="mouseMove">
			<div></div>
			<canvas></canvas>
			<h1>Тебе здесь не место. Возвращайся на <a href="/">главную</a>.</h1>
        </div>
        <v-app v-else>
            <v-app-bar
                color="deep-purple accent-4"
                elevation="24"
                dense
                dark
                class="flex-grow-0"
            >
                <v-app-bar-nav-icon @click.stop="drawer = !drawer"></v-app-bar-nav-icon>
                <v-spacer></v-spacer>
                <v-menu
                    bottom
                    left
                >
                    <template v-slot:activator="{ on, attrs }">
                        <v-btn
                                tile
                                v-bind="attrs"
                                v-on="on"
                        >
                            <v-icon left>
                                mdi-account-circle
                            </v-icon>
                            Добро пожаловать, Администратор
                        </v-btn>
                    </template>
                    <v-list>
                        <v-list-item-group
                                active-class="deep-purple--text text--accent-4"
                        >
                            <v-list-item @click="logout">
                                <v-list-item-icon>
                                    <v-icon>mdi-logout</v-icon>
                                </v-list-item-icon>
                                <v-list-item-content>
                                    <v-list-item-title class="text-left">Выход</v-list-item-title>
                                </v-list-item-content>
                            </v-list-item>
                        </v-list-item-group>
                    </v-list>
                </v-menu>
            </v-app-bar>
            <v-navigation-drawer
                v-model="drawer"
                absolute
                bottom
                temporary
            >
                <v-list
                    flat
                    nav
                    dense
                >
                    <v-subheader>Управление приложением</v-subheader>
                    <v-divider></v-divider>
                    <v-list-item-group
                        active-class="deep-purple--text text--accent-4"
                    >
                        <v-list-item @click="hideSidebar"
                            v-for="(item, i) in items"
                            :key="i"
                            :to="item.path">
                            <v-list-item-icon>
                                <v-icon v-text="item.icon"></v-icon>
                            </v-list-item-icon>
                            <v-list-item-content>
                                <v-list-item-title class="text-left" v-text="item.text"></v-list-item-title>
                            </v-list-item-content>
                        </v-list-item>
                    </v-list-item-group>
                    <v-divider></v-divider>
                    <v-list-item-group
                        :value="false"
                        prepend-icon="mdi-tools"
                    >
                            <v-subheader>Служебные</v-subheader>
                        <v-list-item
                                v-for="([title, icon, path], i) in services"
                                :key="i"
                                link
                                :to="path"
                                @click="hideSidebar"
                        >
                            <v-list-item-icon>
                                <v-icon v-text="icon"></v-icon>
                            </v-list-item-icon>
                            <v-list-item-title class="text-left" v-text="title"></v-list-item-title>
                        </v-list-item>
                    </v-list-item-group>
                </v-list>
            </v-navigation-drawer>
            <v-main>
                <router-view/>
            </v-main>
        </v-app>
    </div>
</template>

<script>
export default {
    name: "AdminPageView",
    data() {
        return {
            userAuthorized: false,
            drawer: false,
            items: [
                {text : "Этажи", icon : "mdi-floor-plan", path: '/adminPanel/floors'},
                {text : "Преподаватели", icon : "mdi-account-school", path: '/adminPanel/teachers'},
                {text : "Аудитории", icon : "mdi-cellphone-link", path: '/adminPanel/auditoriums'},
                {text : "Вторичные объекты", icon : "mdi-stairs", path: '/adminPanel/secondaryObjects'},
                {text : "Пары", icon : "mdi-calendar-check", path: '/adminPanel/pairs'},
                {text : "Дисциплины", icon : "mdi-playlist-check", path: '/adminPanel/disciplines'},
                {text : "Курсы", icon : "mdi-star", path: '/adminPanel/courses'},
                {text : "Группы", icon : "mdi-account-multiple", path: '/adminPanel/groups'}
            ],
            services: [
                ['Ученые звания', 'mdi-trophy-award', '/adminPanel/scienceRanks'],
                ['Информация о парах', 'mdi-information-variant', '/adminPanel/pairInfos'],
                ['Типы вторичных объектов', 'mdi-map-legend', '/adminPanel/secondaryObjectTypes'],
                ['Назначение дисциплин', 'mdi-clipboard-check', '/adminPanel/teachersDisciplines']
            ],
			/*animation stuff*/
			DISPLAY_WIDTH: window.innerWidth,
			DISPLAY_HEIGHT: window.innerHeight,
			DISPLAY_DURATION: 10,
			OVERLAY_DURATION: 3,
			mouse: null,
			container: null,
			overlay: null,
			overlayOpacity: 1,
			canvas: null,
			context: null,
			startTime: null,
			eyes: []
        }
    },
    async mounted() {
		document.title = "Страница администратора";
        this.userAuthorized = await this.$oidc.isAdmin();
		if (!this.userAuthorized) {
			this.mouse = {
				x: this.DISPLAY_WIDTH / 2,
				y: this.DISPLAY_HEIGHT / 2
			}
			this.initialize();
		}
    },
    async updated() {
        this.userAuthorized = await this.$oidc.isAdmin();
    },
    methods: {
        logout() {
            this.$oidc.logout();
        },
        hideSidebar() {
            this.drawer = false;
        },
		initialize() {
			this.container = document.getElementById( 'screwOffContainer');
			this.overlay = document.querySelector('#screwOffContainer>div');
			this.canvas = document.querySelector('#screwOffContainer>canvas');

			this.header = document.querySelector('#screwOffContainer>h1');
			if (this.header)
				this.header.className += ' show';

			if (this.canvas) {
				this.canvas.style.width = window.innerWidth + 'px';
				this.canvas.style.height = window.innerHeight + 'px';
				this.canvas.width = window.innerWidth;
				this.canvas.height = window.innerHeight;
				this.context = this.canvas.getContext( '2d' );
				this.eyes = [
					new Eye( this.canvas,   0.19, 0.80,   0.88,    0.31 ),
					new Eye( this.canvas,   0.10, 0.54,   0.84,    0.32 ),
					new Eye( this.canvas,   0.81, 0.13,   0.63,    0.33 ),
					new Eye( this.canvas,   0.89, 0.19,   0.58,    0.34 ),
					new Eye( this.canvas,   0.40, 0.08,   0.97,    0.35 ),
					new Eye( this.canvas,   0.64, 0.74,   0.57,    0.36 ),
					new Eye( this.canvas,   0.41, 0.89,   0.56,    0.37 ),
					new Eye( this.canvas,   0.92, 0.89,   0.75,    0.38 ),
					new Eye( this.canvas,   0.27, 0.20,   0.87,    0.39 ),
					new Eye( this.canvas,   0.17, 0.46,   0.68,    0.41 ),
					new Eye( this.canvas,   0.71, 0.29,   0.93,    0.42 ),
					new Eye( this.canvas,   0.84, 0.46,   0.54,    0.43 ),
					new Eye( this.canvas,   0.93, 0.35,   0.63,    0.44 ),
					new Eye( this.canvas,   0.77, 0.82,   0.85,    0.45 ),
					new Eye( this.canvas,   0.36, 0.74,   0.90,    0.46 ),
					new Eye( this.canvas,   0.13, 0.24,   0.85,    0.47 ),
					new Eye( this.canvas,   0.58, 0.20,   0.77,    0.48 ),
					new Eye( this.canvas,   0.55, 0.84,   0.87,    0.50 ),

					new Eye( this.canvas,   0.50, 0.50,   5.00,    0.10 )
				];
				this.startTime = Date.now();
				this.animate();

				function Eye( canvas, x, y, scale, time ) {
					this.canvas = canvas;
					this.context = this.canvas.getContext('2d')
					this.activationTime = time;
					this.irisSpeed = 0.01 + (Math.random() * 0.2) / scale;
					this.blinkSpeed = 0.2 + (Math.random() * 0.2);
					this.blinkInterval = 5000 + 5000 * (Math.random());
					this.blinkTime = Date.now();
					this.scale = scale;
					this.size = 70 * scale;
					this.x = x * canvas.width;
					this.y = y * canvas.height + (this.size * 0.15);
					this.iris = {
						x: this.x,
						y: this.y - (this.size * 0.1),
						size: this.size * 0.2
					};
					this.pupil = {
						width: 2 * scale,
						height: this.iris.size * 0.75
					};
					this.exposure = {
						top: 0.1 + (Math.random() * 0.3),
						bottom: 0.5 + (Math.random() * 0.3),
						current: 0,
						target: 1
					};
					this.tiredness = (0.5 - this.exposure.top) + 0.1;
					this.isActive = false;
					this.activate = function () {
						this.isActive = true;
					}
					this.update = function (mouse) {
						if (this.isActive === true) {
							this.render(mouse);
						}
					}
					this.render = function (mouse) {
						let time = Date.now();
						if (this.exposure.current < 0.012) {
							this.exposure.target = 1;
						} else if (time - this.blinkTime > this.blinkInterval) {
							this.exposure.target = 0;
							this.blinkTime = time;
						}
						this.exposure.current += (this.exposure.target - this.exposure.current) * this.blinkSpeed;
						let el = {x: this.x - (this.size * 0.8), y: this.y - (this.size * 0.1)};
						let er = {x: this.x + (this.size * 0.8), y: this.y - (this.size * 0.1)};
						let et = {
							x: this.x,
							y: this.y - (this.size * (0.5 + (this.exposure.top * this.exposure.current)))
						};
						let eb = {
							x: this.x,
							y: this.y - (this.size * (0.5 - (this.exposure.bottom * this.exposure.current)))
						};
						let eit = {
							x: this.x,
							y: this.y - (this.size * (0.5 + ((0.5 - this.tiredness) * this.exposure.current)))
						};
						let ei = {x: this.x, y: this.y - (this.iris.size)};
						let eio = {
							x: (mouse.x / window.innerWidth) - 0.5,
							y: (mouse.y / window.innerHeight) - 0.5
						};
						ei.x += eio.x * 16 * Math.max(1, this.scale * 0.4);
						ei.y += eio.y * 10 * Math.max(1, this.scale * 0.4);
						this.iris.x += (ei.x - this.iris.x) * this.irisSpeed;
						this.iris.y += (ei.y - this.iris.y) * this.irisSpeed;
						this.context.fillStyle = 'rgba(255,255,255,1.0)';
						this.context.strokeStyle = 'rgba(100,100,100,1.0)';
						this.context.beginPath();
						this.context.lineWidth = 3;
						this.context.lineJoin = 'round';
						this.context.moveTo(el.x, el.y);
						this.context.quadraticCurveTo(et.x, et.y, er.x, er.y);
						this.context.quadraticCurveTo(eb.x, eb.y, el.x, el.y);
						this.context.closePath();
						this.context.stroke();
						this.context.fill();
						this.context.save();
						this.context.globalCompositeOperation = 'source-atop';
						this.context.translate(this.iris.x * 0.1, 0);
						this.context.scale(0.9, 1);
						this.context.strokeStyle = 'rgba(0,0,0,0.5)';
						this.context.fillStyle = 'rgba(130,50,90,0.9)';
						this.context.lineWidth = 2;
						this.context.beginPath();
						this.context.arc(this.iris.x, this.iris.y, this.iris.size, 0, Math.PI * 2, true);
						this.context.fill();
						this.context.stroke();
						this.context.restore();
						this.context.save();
						this.context.shadowColor = 'rgba(255,255,255,0.5)';
						this.context.shadowOffsetX = 0;
						this.context.shadowOffsetY = 0;
						this.context.shadowBlur = 2 * this.scale;
						this.context.globalCompositeOperation = 'source-atop';
						this.context.translate(this.iris.x * 0.1, 0);
						this.context.scale(0.9, 1);
						this.context.fillStyle = 'rgba(255,255,255,0.2)';
						this.context.beginPath();
						this.context.arc(this.iris.x, this.iris.y, this.iris.size * 0.7, 0, Math.PI * 2, true);
						this.context.fill();
						this.context.restore();
						this.context.save();
						this.context.globalCompositeOperation = 'source-atop';
						this.context.fillStyle = 'rgba(0,0,0,0.9)';
						this.context.beginPath();
						this.context.moveTo(this.iris.x, this.iris.y - (this.pupil.height * 0.5));
						this.context.quadraticCurveTo(this.iris.x + (this.pupil.width * 0.5), this.iris.y, this.iris.x, this.iris.y + (this.pupil.height * 0.5));
						this.context.quadraticCurveTo(this.iris.x - (this.pupil.width * 0.5), this.iris.y, this.iris.x, this.iris.y - (this.pupil.height * 0.5));
						this.context.fill();
						this.context.restore();
						this.context.save();
						this.context.shadowColor = 'rgba(0,0,0,0.9)';
						this.context.shadowOffsetX = 0;
						this.context.shadowOffsetY = 0;
						this.context.shadowBlur = 10;
					}
				}
			}
			else if(this.overlay) {
				this.overlay.parentElement.removeChild(this.overlay);
			}
		},
		animate() {
			let seconds = (Date.now() - this.startTime) / 1000;
			this.context.clearRect( 0, 0, this.DISPLAY_WIDTH, this.DISPLAY_HEIGHT);
			for (let i = 0; i < this.eyes.length; i++) {
				let eye = this.eyes[i];
				if (seconds > this.eyes[i].activationTime * this.DISPLAY_DURATION) {
					eye.activate();
				}
				eye.update(this.mouse);
			}
			if (seconds > this.OVERLAY_DURATION && this.overlay !== undefined ) {
				this.overlayOpacity *= 0.94 + ( 0.055 * this.overlayOpacity );
				this.overlayOpacity = Math.max( this.overlayOpacity - 0.01, 0 );
				this.overlay.style.opacity = this.overlayOpacity;
				if (this.overlayOpacity === 0 ) {
					this.container.removeChild(this.overlay );
					this.overlay = undefined;
				}
			}
			requestAnimationFrame(this.animate);
		},
		mouseMove() {
			this.mouse.x = event.clientX;
			this.mouse.y = event.clientY;
		}
    }
}
</script>

<style scoped>
    v-list-item-title {
        text-align: left;
    }
	#screwOffContainer {
		position: fixed;
		height: 100%;
		width: 100%;
		margin-bottom: 120px;
		text-align: center;
		display: -webkit-box;
		display: -webkit-flex;
		display: -ms-flexbox;
		display: flex;
		-webkit-box-orient: vertical;
		-webkit-box-direction: normal;
		-webkit-flex-direction: column;
		-ms-flex-direction: column;
		flex-direction: column;
		-webkit-box-pack: center;
		-webkit-justify-content: center;
		-ms-flex-pack: center;
		justify-content: center;
	}
	#screwOffContainer div {
		position: fixed;
		width: 100%;
		height: 100%;
		top: 0;
		left: 0;
		background: #000;
	}
	#screwOffContainer canvas {
		z-index: 1;
		background: #000;
	}
	#screwOffContainer h1 {
		position: absolute;
		width: 100%;
		bottom: 20px;
		left: 0;
		z-index: 2;
		color: white;
		text-align: center;
		opacity: 0;
		font-size: 28px;
		transition: opacity 1s ease 3s;
	}
	#screwOffContainer h1.show {
		opacity: 1;
	}
	#screwOffContainer h1.show a{
		color: white;
	}
</style>