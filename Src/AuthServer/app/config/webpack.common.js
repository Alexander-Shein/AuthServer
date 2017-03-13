let webpack = require('webpack'),
    HtmlWebpackPlugin = require('html-webpack-plugin'),
    ExtractTextPlugin = require('extract-text-webpack-plugin'),
    helpers = require('./helpers');

module.exports = {
    entry: {
        'polyfills': './src/polyfills.ts',
        'vendor': './src/vendor.ts',
        'app': './src/main.ts'
    },

    resolve: {
        extensions: ['.ts', '.js']
    },

    module: {
        rules: [
            {
                test: /\.ts$/,
                use: [ 'awesome-typescript-loader', 'angular2-template-loader' ]
            },
            {
                test: /\.html$/,
                use: 'html-loader'
            },
            {
                test: /.*\.(gif|png|jpe?g|svg)$/i,
                loaders: [
                    {
                        loader: 'url-loader',
                        query: {
                            name: 'assets/[name].[hash].[ext]',
                            limit: '10000'
                        }
                    },
                    {
                        loader: 'image-webpack-loader',
                        query: {
                            progressive: true,
                            pngquant: {
                                quality: '65-90',
                                speed: 4
                            },
                            mozjpeg: {quality: 65}
                        }
                    }]
            },
            {
                test: /\.(woff|woff2|ttf|eot|ico)(\?v=[0-9]\.[0-9]\.[0-9])?$/,
                use: [{
                    loader: 'url-loader',
                    query: {
                        name: 'assets/[name].[hash].[ext]',
                        limit: '10000'
                    }
                }]
            },
            {
                test: /\.css$/,
                exclude: helpers.root('src', 'components'),
                loader: ExtractTextPlugin.extract({
                    fallbackLoader: 'style-loader',
                    loader: ['css-loader', 'postcss-loader']
                })
            },
            {
                test: /\.css$/,
                include: helpers.root('src', 'components'),
                use: ['raw-loader', 'postcss-loader']
            },
            {
                test: /\.scss$/,
                exclude: helpers.root('src', 'components'),
                loader: ExtractTextPlugin.extract({
                    fallbackLoader: 'style-loader',
                    loader: ['css-loader', 'postcss-loader', 'sass-loader']
                })
            },
            {
                test: /\.scss$/,
                include: helpers.root('src', 'components'),
                use: ['raw-loader', 'postcss-loader', 'sass-loader']
            }
        ]
    },

    plugins: [
        // Workaround for angular/angular#11580
        new webpack.ContextReplacementPlugin(
            // The (\\|\/) piece accounts for path separators in *nix and Windows
            /angular(\\|\/)core(\\|\/)(esm(\\|\/)src|src)(\\|\/)linker/,
            helpers.root('./src'), // location of your src
            {} // a map of your routes
        ),

        new webpack.optimize.CommonsChunkPlugin({
            name: ['app', 'vendor', 'polyfills']
        }),

        new HtmlWebpackPlugin({
            template: './src/index.html',
            chunksSortMode: 'dependency'
        })
    ]
};
