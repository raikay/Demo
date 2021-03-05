module.exports = {
    title: '优惠券接口文档',
    description: 'Hello, my friend!',
	base:'/api-doc/',
    head: [
        ['link', {
            rel: 'icon',
            href: `/hero.png`
        }]
    ],
    dest: './dist',
    ga: '',
    evergreen: true,
	themeConfig: {
        nav: [
          { text: 'Home', link: '/' },
          { text: '接口文档', link: '/API2/' },
          {
            text: 'Languages',
            items: [
              { text: 'Chinese', link: '/language/chinese' },
              { text: 'English', link: '/language/english' }
            ]
          },
          { text: 'External', link: 'https://www.baidu.com' },
        ],
		displayAllHeaders: true, // 默认值：false
		sidebarDepth: 2,
		sidebar: [
      '/api2/',
      '/api3/',
	  '/api4/',
'/api5/'
		

		]
		

    }
}
