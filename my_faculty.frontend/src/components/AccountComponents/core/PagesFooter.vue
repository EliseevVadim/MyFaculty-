<template>
	<v-footer
		dark
		padless
	>
		<v-card
			class="flex"
			flat
			tile
			color="indigo darken-3"
		>
			<v-card-title>
				<strong class="subheading">Приятного пользования! Мы в соцсетях:</strong>
				<v-spacer></v-spacer>
				<a href="https://vk.com/fcl_phys" class="social-link mx-4" style="margin-top: 14px">
					<svg fill="white" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 448 512" width="24" height="24">
						<path d="M31.4907 63.4907C0 94.9813 0 145.671 0 247.04V264.96C0 366.329 0 417.019 31.4907 448.509C62.9813 480 113.671 480 215.04 480H232.96C334.329 480 385.019 480 416.509 448.509C448 417.019 448 366.329 448 264.96V247.04C448 145.671 448 94.9813 416.509 63.4907C385.019 32 334.329 32 232.96 32H215.04C113.671 32 62.9813 32 31.4907 63.4907ZM75.6 168.267H126.747C128.427 253.76 166.133 289.973 196 297.44V168.267H244.16V242C273.653 238.827 304.64 205.227 315.093 168.267H363.253C359.313 187.435 351.46 205.583 340.186 221.579C328.913 237.574 314.461 251.071 297.733 261.227C316.41 270.499 332.907 283.63 346.132 299.751C359.357 315.873 369.01 334.618 374.453 354.747H321.44C316.555 337.262 306.614 321.61 292.865 309.754C279.117 297.899 262.173 290.368 244.16 288.107V354.747H238.373C136.267 354.747 78.0267 284.747 75.6 168.267Z"/></svg>
				</a>
				<a
					v-for="(item, i) in socials"
					:href="item.link"
					:key="i"
					class="mx-4 social-link"
				>
					<v-icon size="24px">
						{{ item.icon }}
					</v-icon>
				</a>
			</v-card-title>
			<v-card-text class="py-2 white--text text-center">
                    <span class="credential">
                        {{ new Date().getFullYear() }}
                    </span>
				— <strong>
				<a href="https://vk.com/lord_ajdis">TheArchitect</a>
			</strong>
			</v-card-text>
		</v-card>
	</v-footer>
</template>

<script>
export default {
	name: "PagesFooter",
	data() {
		return {
			socials: [
				{ "icon" : 'mdi-facebook', "link" : "#" },
				{ "icon" : 'mdi-linkedin', "link" : "#" },
				{ "icon" : 'mdi-twitter', "link" : "#" },
				{ "icon" : 'mdi-instagram', "link" : "#" }
			],
		}
	}
}
</script>

<style scoped>
	.social-link {
		text-decoration: none;
	}
	strong > a, .credential {
		color: white;
		font-weight: bold;
		font-size: 18px;
	}
</style>